using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App7
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            // создание таблицы, если ее нет
            await App.Database.CreateTable();
            // привязка данных
            machineList.ItemsSource = await App.Database.GetItemsAsync();

            base.OnAppearing();
        }
        // обработка нажатия элемента в списке
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            DataMachine selectedMachine = (DataMachine)e.SelectedItem;
            MachinePage MachinePage = new MachinePage();
            MachinePage.BindingContext = selectedMachine;
            await Navigation.PushAsync(MachinePage);
        }
        // обработка нажатия кнопки добавления
        private async void CreateMachine(object sender, EventArgs e)
        {
            DataMachine machine = new DataMachine();
            MachinePage machinePage = new MachinePage();
            machinePage.BindingContext = machine;
            await Navigation.PushAsync(machinePage);
        }
    }
}
