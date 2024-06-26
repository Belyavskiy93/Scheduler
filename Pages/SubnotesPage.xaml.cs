namespace Scheduler;


public partial class SubnotesPage : ContentPage
{
    static DisplaySubnotes display;
    AbstractNote route_note;
    AbstractNote old;
    public SubnotesPage(AbstractNote note)
	{
        InitializeComponent();
        
        route_note = note;
        Title= route_note.Name;
        
        string path = AppDomain.CurrentDomain.BaseDirectory;
        string container = Path.Combine(path, "Subnotes.txt");

        if (!File.Exists(container))
        {
            FileStream file = File.Create(container);
        }

        AbstractDocument document = new TextDocument(container);
        display = new DisplaySubnotes(new TextDocumentWriter(document),new TextDocumentReader(document,"Subnote"), 
                                      new TextDocumentEditor(document),route_note);

        BindingContext = display;
    }

    private async void Add_Subnote(object sender, EventArgs e)
    {
        FormPage form = new FormPage(this,route_note.Id);
        await Navigation.PushAsync(form); 
    }

    private async void Edit_Subnote(object sender, EventArgs e)
    {
        MenuItem item = sender as MenuItem;
        display.Selected_note = item.BindingContext as AbstractNote;
        old = new Subnote();
        AbstractNote.CopyAbstractNote(display.Selected_note, old);

        FormPage form = new FormPage(this,route_note.Id,display.Selected_note);
        await Navigation.PushAsync(form);
    }

    private void Delete_Subnote(object sender, EventArgs e)
    {
        MenuItem item = sender as MenuItem;
        display.Selected_note = item.BindingContext as AbstractNote;

        display.Delete(display.Selected_note);
    }


    // Deletes sublist of the specifed Note. Used to delete Note and all associated data
    internal void Delete()
    {
        for(int i = 0; i < display.current_list.Count; ++i)
        {
            display.Delete(display.current_list[i]);
            --i;
        }
    }

    // Used to create Subnote from FormPage
    internal void Add(AbstractNote note)
    {
        display.Add(note);
    }

    //Used to edit Subnote from FormPage
    internal void Change(AbstractNote update_note)
    {
        display.Edit(old, update_note);
    }
}