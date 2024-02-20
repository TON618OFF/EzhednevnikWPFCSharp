using practice_2;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace practice_2
{

    public partial class MainWindow : Window
    {
        public ObservableCollection<Notes> Notes { get; set; } = new ObservableCollection<Notes>();

        private Notes selectedNote;

        public MainWindow(ObservableCollection<Notes> notes)
        {
            InitializeComponent();
            DatePickerBlock.SelectedDate = DateTime.Today;
            Notes.ItemsSource = Notes;
            LoadNotes();
        }

        private void SaveNotes() 
        {

            JsonSerializationDeserialization.Serialize(Notes, "notes.json");

        }

        private void LoadNotes() 
        {

            Notes.Clear();

            IEnumerable<Notes> loadedNotes = JsonSerializationDeserialization.Deserialize<Notes>("notes.json");

            if (loadedNotes != null)
            {

                foreach (var note in loadedNotes)
                {

                    Notes.Add(note);

                }

                DatePicker_SelectedDateChanged(null, null);

            }

        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) 
        {

            DateTime date = DatePickerBlock.SelectedDate ?? DateTime.Today;
            var filteredNotes = Notes.Where(n => n.Date.Date == date.Date).ToList();
            Notes.ItemsSource = filteredNotes;

        }

        private void CreateNote(object sender, RoutedEventArgs e) 
        {

            string title = maintext.Text.Trim(); // Удаляет все начальные и конечные символы пробела из текущей строки.
            string description = secondary_text.Text.Trim(); // Удаляет все начальные и конечные символы пробела из текущей строки.
            DateTime date = DatePickerBlock.SelectedDate ?? DateTime.Today; // возвращает значение своего операнда слева, если его значение не равно null

            if (!string.IsNullOrWhiteSpace(title) || !string.IsNullOrWhiteSpace(description))

            {

                Notes newNote = new Notes(title, description, date); 
                Notes.Add(newNote);
                DatePicker_SelectedDateChanged(null, null);
                SaveNotes();

            }

        }

        private void DeleteNote(object sender, RoutedEventArgs e) 
        {
            if (selectedNote != null)
            {

                Notes.Remove(selectedNote);
                DatePicker_SelectedDateChanged(null, null);
                SaveNotes();

            }
        }

        private void SaveInfo(object sender, RoutedEventArgs e) 
        {
            if (selectedNote != null)
            {

                selectedNote.Title = maintext.Text;
                selectedNote.Description = secondary_text.Text;
                Notes.items.Refresh();
                SaveNotes();

            }
        }
        private void note_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            selectedNote = Notes.SelectedItem as Notes;
            if (selectedNote != null)
            {

                maintext.Text = selectedNote.Title;
                secondary_text.Text = selectedNote.Description;

            }

        }

    }

}