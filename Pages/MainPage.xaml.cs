using System.Collections.ObjectModel;

namespace Scheduler;

public partial class MainPage : ContentPage
{
    static DisplayNotes display;
    SubnotesPage sub_page;
    
    public MainPage()
    {
        InitializeComponent();
        
        string path = AppDomain.CurrentDomain.BaseDirectory;
        string container = Path.Combine(path,"Notes.txt");
        
 
        if (!File.Exists(container))
        {
            FileStream file = File.Create(container);
        }

        AbstractDocument document = new TextDocument(container);
        display = new DisplayNotes(new TextDocumentWriter(document), new TextDocumentReader(document, "Note"),new TextDocumentEditor(document));

        BindingContext = display;
    }

    private async void Add_Note(object sender, EventArgs e)
    {
        string name = await DisplayPromptAsync("Enter the name: ", "", "OK", "Cancel");

        if(name != null)
        {
            if(name.Contains('<') || name.Contains('>'))
            {
                await DisplayAlert("Error", "Forbidden symbols:'<','>'", "OK");
            }
            else
            {
                Note note = new Note(name);
                display.Add(note);
            }
        }
    }

    private async void Open_Sublist(object sender, ItemTappedEventArgs e)
    {
        sub_page = new SubnotesPage(display.Selected_note);
        await Navigation.PushAsync(sub_page);
        list.SelectedItem = null;
    }

    private void Delete_Note(object sender, EventArgs e)
    {
        MenuItem? menu = sender as MenuItem;
        display.Selected_note = menu.BindingContext as AbstractNote;

        sub_page = new SubnotesPage(display.Selected_note);
        sub_page.Delete();

        display.Delete(display.Selected_note);
    }

    private async void Edit_Note(object sender, EventArgs e)
    {
        MenuItem? menu = sender as MenuItem;
        display.Selected_note = menu.BindingContext as AbstractNote;
        AbstractNote old = new Note();

        AbstractNote.CopyAbstractNote(display.Selected_note, old);

        string name = await DisplayPromptAsync("Enter new name: ", "New name: ", "Ok", "Cancel", display.Selected_note.Name);

        if (name != null)
        {
            if (name.Contains('<') || name.Contains('>'))
            {
                DisplayAlert("Error", "Forbidden symbol:'<','>'", "OK");
            }
            else
            {
                display.Selected_note.Name = name;
                display.Edit(old, display.Selected_note);
                
            }
           
        }
        list.SelectedItem = null;
    }
}


