namespace Scheduler;

public partial class FormPage : ContentPage
{
	SubnotesPage page;
	AbstractNote target_note;
	public FormPage(SubnotesPage page,string id, AbstractNote note = null)
	{
		InitializeComponent();
		this.page = page;

		if (note == null)
		{
			target_note = new Subnote(id,"");
			save.Clicked += new EventHandler(SaveData);
		}
		else
		{
			target_note = note;
			save.Clicked += new EventHandler(ChangeData);
			
		}

		BindingContext = target_note;
	}

	// Creates new Subnote
    private void SaveData(object sender, EventArgs e)
    {
		page.Add(target_note);
		Navigation.PopAsync();
    }

	//Edits Subnote
	private void ChangeData(object sender,EventArgs e)
	{
		page.Change(target_note);
		Navigation.PopAsync();
	}

    private void ValidationEntryText(object sender, TextChangedEventArgs e)
    {
		Entry entry = sender as Entry;

		if(entry.Text.Contains('<') || entry.Text.Contains('>'))
		{
			entry.Text = e.OldTextValue;
			DisplayAlert("Error", "Forbidden symbols:'<','>'", "OK");

		}
    }

    private void ValidationEditorText(object sender, TextChangedEventArgs e)
    {
		Editor editor = sender as Editor;

        if (editor.Text.Contains('<') || editor.Text.Contains('>'))
        {
            editor.Text = e.OldTextValue;
            DisplayAlert("Error", "Forbidden symbols:'<','>'", "OK");

        }

    }
}