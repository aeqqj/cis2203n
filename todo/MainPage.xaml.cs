using System.Collections.ObjectModel;

namespace todo;

public partial class MainPage : ContentPage
{
    private ObservableCollection<ToDoClass> _todoItems = new();
    private ToDoClass? _selectedItem = null;
    private int _nextId = 1;

    public MainPage()
    {
        InitializeComponent();
        todoLV.ItemsSource = _todoItems;
    }

    private void AddToDoItem(object sender, EventArgs e)
    {
        string title = titleEntry.Text?.Trim() ?? "";
        string detail = detailsEditor.Text?.Trim() ?? "";

        if (string.IsNullOrEmpty(title))
        {
            DisplayAlert("HEY", "WHERE TITLE AT!?", "OK");
            return;
        }

        _todoItems.Add(new ToDoClass
        {
            id = _nextId++,
            title = title,
            detail = detail
        });

        ClearForm();
    }

    private void EditToDoItem(object sender, EventArgs e)
    {
        if (_selectedItem == null) return;

        string title = titleEntry.Text?.Trim() ?? "";
        string detail = detailsEditor.Text?.Trim() ?? "";

        if (string.IsNullOrEmpty(title))
        {
            DisplayAlert("HEEEEEEYY", "WHERE TITLE !! TITLE CANNOT BE EMPTY !!", "OK");
            return;
        }

        _selectedItem.title = title;
        _selectedItem.detail = detail;

        var temp = new ObservableCollection<ToDoClass>(_todoItems);
        _todoItems.Clear();
        foreach (var item in temp) _todoItems.Add(item);

        ClearForm();
    }

    private void CancelEdit(object sender, EventArgs e)
    {
        ClearForm();
    }

    private void DeleteToDoItem(object sender, EventArgs e)
    {
        if (sender is Button btn && int.TryParse(btn.ClassId, out int id))
        {
            var item = _todoItems.FirstOrDefault(t => t.id == id);
            if (item != null)
                _todoItems.Remove(item);

            if (_selectedItem?.id == id)
                ClearForm();
        }
    }

    private void TodoLV_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
    }

    private void todoLV_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is not ToDoClass tapped) return;

        _selectedItem = tapped;
        titleEntry.Text = tapped.title;
        detailsEditor.Text = tapped.detail;

        addBtn.IsVisible = false;
        editBtn.IsVisible = true;
        cancelBtn.IsVisible = true;

        todoLV.SelectedItem = null;
    }

    private void ClearForm()
    {
        titleEntry.Text = string.Empty;
        detailsEditor.Text = string.Empty;
        _selectedItem = null;

        addBtn.IsVisible = true;
        editBtn.IsVisible = false;
        cancelBtn.IsVisible = false;
    }
}
