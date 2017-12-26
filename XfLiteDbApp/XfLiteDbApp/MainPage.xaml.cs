using LiteDB;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XfLiteDbApp
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        private async void Create_Click(object sender, EventArgs e)
        {
            // Use PCL Storage
            var folder = FileSystem.Current.LocalStorage;
            var subFolder = await folder.CreateFolderAsync("XfLiteDbApp", CreationCollisionOption.OpenIfExists);
            var file = await subFolder.CreateFileAsync("PersonData.db", CreationCollisionOption.ReplaceExisting);
            var dbstring = file.Path;


            using (var db = new LiteDatabase(dbstring))
            {
                // Get datamodel collection
                var datas = db.GetCollection<DataModel>("persondata");

                // Create new DataModel instance
                var data = new ObservableCollection<DataModel>()
                {
                    new DataModel(){LastName = "紫山", FirstName = "太郎", Height = (float)167.5, Weight = (float)69.1 },
                    new DataModel(){LastName = "寺岡", FirstName = "次郎", Height = (float)193.0, Weight = (float)82.5 },
                    new DataModel(){LastName = "高森", FirstName = "三郎", Height = (float)174.8, Weight = (float)75.6 },
                    new DataModel(){LastName = "桂", FirstName = "四郎", Height = (float)186.3, Weight = (float)93.7 }
                };

                // Insert
                datas.Insert(data);

            }
        }

        private async void List_Click(object sender, EventArgs e)
        {
            // Use PCL Storage
            var folder = FileSystem.Current.LocalStorage;
            var subFolder = await folder.CreateFolderAsync("XfLiteDbApp", CreationCollisionOption.OpenIfExists);
            var file = await subFolder.GetFileAsync("PersonData.db");
            var dbstring = file.Path;

            using (var db = new LiteDatabase(dbstring))
            {
                // Get datamodel collection
                var datas = db.GetCollection<DataModel>("persondata");

                // Read
                var datasource = datas.FindAll().ToList();

                list.ItemsSource = datasource;
            }
        }
    }
}
