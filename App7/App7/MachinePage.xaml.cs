using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App7
{
    public partial class MachinePage : ContentPage
    {
        public MachinePage()
        {
            InitializeComponent();
        }
        private async void SaveMachine(object sender, EventArgs e)
        {
            var machine = (DataMachine)BindingContext;
            if (!String.IsNullOrEmpty(machine.Name))
            {
                await App.Database.SaveItemAsync(machine);
            }
            await this.Navigation.PopAsync();
        }
        private async void DeleteMachine(object sender, EventArgs e)
        {
            var machine = (DataMachine)BindingContext;
            await App.Database.DeleteItemAsync(machine);
            await this.Navigation.PopAsync();
        }
        private async void Cancel(object sender, EventArgs e)
        {
            await this.Navigation.PopAsync();
        }
        private async void PickAndDownloadPdf()
        {
            try
            {
                // Открываем файловый менеджер
                FileData fileData = await CrossFilePicker.Current.PickFile(); //PickFile(new[] { ".pdf" });

                if (fileData != null)
                {
                    // Сохраняем файл
                    var path = Path.Combine(FileSystem.AppDataDirectory, fileData.FileName);
                    using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        await stream.WriteAsync(fileData.DataArray, 0, fileData.DataArray.Length);
                    }

                    await DisplayAlert("Успех", "Файл успешно загружен!", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }
        private void OnSelectPdfClicked(object sender, EventArgs e)
        {
            PickAndDownloadPdf();
        }
    }
}