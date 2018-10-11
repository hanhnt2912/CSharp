using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ExT1708M1.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccountHandle : Page
    {

        private Account currentAccount;
        public AccountHandle()
        {
            this.InitializeComponent();
            this.currentAccount = new Account();
        }

        private async void BtbSave_OnClick(object sender, RoutedEventArgs e)
        {
            this.currentAccount.Name = this.name.Text;
            this.currentAccount.Email = this.email.Text;
            this.currentAccount.Phone = this.phone.Text;
            string jsonAccount = JsonConvert.SerializeObject(this.currentAccount);

            var savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation =
                PickerLocationId.DocumentsLibrary;
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "New File";
            StorageFile file = await savePicker.PickSaveFileAsync();
            await FileIO.WriteTextAsync(file, jsonAccount);
        }

        private async void BtnLoad_OnClick(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.FileTypeFilter.Add(".txt");
            StorageFile file = await openPicker.PickSingleFileAsync();

            string content = await FileIO.ReadTextAsync(file);
             Account account =JsonConvert.DeserializeObject<Account>(content);
           name.Text = account.Name;
            email.Text = account.Email;
            phone.Text = account.Phone;


        }
    }
}
