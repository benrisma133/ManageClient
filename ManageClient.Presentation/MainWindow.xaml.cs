using System.Windows;
using System.Windows.Controls;
using ManageClient.Repository;
using ManageClient.Repository.Models;
using ManageClient.Service;

namespace ManageClient.Presentation;

public partial class MainWindow : Window
{
    private ClientService _clientService = new ClientService();

    public MainWindow()
    {
        InitializeComponent();
        DatabaseHelper.InitializeDatabase();
        LoadClients();
    }

    private void LoadClients()
    {
        DgClients.ItemsSource = _clientService.GetAll();
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TxtFullName.Text) ||
            string.IsNullOrWhiteSpace(TxtPhone.Text) ||
            string.IsNullOrWhiteSpace(TxtEmail.Text))
        {
            MessageBox.Show("Please fill all fields.", "Validation",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        _clientService.Client.FullName = TxtFullName.Text.Trim();
        _clientService.Client.PhoneNumber = TxtPhone.Text.Trim();
        _clientService.Client.Email = TxtEmail.Text.Trim();

        bool success = _clientService.Save();

        if (success)
        {
            MessageBox.Show("Client saved successfully!", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);
            LoadClients();
        }
        else
        {
            MessageBox.Show("Failed to save. Phone or Email already exists.", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (_clientService.Client.Id == 0)
        {
            MessageBox.Show("Please select a client to delete.", "Warning",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var result = MessageBox.Show("Are you sure you want to delete this client?",
            "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            bool deleted = _clientService.Delete();
            if (deleted)
            {
                MessageBox.Show("Client deleted.", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                ClearForm();
                LoadClients();
            }
        }
    }

    private void BtnClear_Click(object sender, RoutedEventArgs e)
    {
        ClearForm();
    }

    private void DgClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (DgClients.SelectedItem is Client selected)
        {
            _clientService = ClientService.Load(selected.Id);
            TxtFullName.Text = _clientService.Client.FullName;
            TxtPhone.Text = _clientService.Client.PhoneNumber;
            TxtEmail.Text = _clientService.Client.Email;
        }
    }

    private void ClearForm()
    {
        _clientService = new ClientService();
        TxtFullName.Text = string.Empty;
        TxtPhone.Text = string.Empty;
        TxtEmail.Text = string.Empty;
        DgClients.SelectedItem = null;
    }

    private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        string query = TxtSearch.Text.Trim().ToLower();

        // Toggle placeholder and clear button
        SearchHint.Visibility = string.IsNullOrEmpty(query) ? Visibility.Visible : Visibility.Collapsed;
        BtnClearSearch.Visibility = string.IsNullOrEmpty(query) ? Visibility.Collapsed : Visibility.Visible;

        var all = _clientService.GetAll();

        DgClients.ItemsSource = string.IsNullOrEmpty(query)
            ? all
            : all.Where(c => c.FullName.ToLower().Contains(query) ||
                             c.PhoneNumber.Contains(query)).ToList();
    }

    private void BtnClearSearch_Click(object sender, RoutedEventArgs e)
    {
        TxtSearch.Text = string.Empty;
        TxtSearch.Focus();
    }

}