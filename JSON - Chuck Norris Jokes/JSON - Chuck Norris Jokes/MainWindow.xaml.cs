using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JSON___Chuck_Norris_Jokes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            comboBox.Items.Add("all");
            comboBox.SelectedIndex = 0;

            using (var client = new HttpClient())
            {
                string jsonData = client.GetStringAsync("https://api.chucknorris.io/jokes/categories").Result;
                List<string> api = JsonConvert.DeserializeObject<List<string>>(jsonData);
                foreach (string item in api)
                {
                    comboBox.Items.Add(item);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string selected = comboBox.SelectedItem.ToString();
            if (selected == "all")
            {
                using (var client = new HttpClient())
                {
                    string jsonData = client.GetStringAsync("http://api.chucknorris.io/jokes/random?").Result;
                    ChuckNorrisAPI api = JsonConvert.DeserializeObject<ChuckNorrisAPI>(jsonData);
                    txtBlock.Text = api.value;
                }
            }
            else
            {
                using (var client = new HttpClient())
                {
                    string jsonData = client.GetStringAsync("http://api.chucknorris.io/jokes/random?category=" + selected).Result;
                    ChuckNorrisAPI api = JsonConvert.DeserializeObject<ChuckNorrisAPI>(jsonData);
                    txtBlock.Text = api.value;
                }
            }
        }
    }
}
