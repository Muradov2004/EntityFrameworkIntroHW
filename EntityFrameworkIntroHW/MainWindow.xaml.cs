using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EntityFrameworkIntroHW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CarContext CarContext = new CarContext();
        public ObservableCollection<Car> cars { get; set; }

        public MainWindow()
        {

            InitializeComponent();

            cars = new ObservableCollection<Car>();
            CarContext.Cars.ToList().ForEach(c => cars.Add(c));

            DataContext = this;

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MarkaTextBox.Text == string.Empty && ModelTextBox.Text == string.Empty && YearTextBox.Text == string.Empty && StNumberTextBox.Text == string.Empty)
            {
                DeleteButton.IsEnabled = false;
                UpdateButton.IsEnabled = false;
            }

        }

        private void ClearTextBox()
        {
            MarkaTextBox.Clear();
            ModelTextBox.Clear();
            YearTextBox.Clear();
            StNumberTextBox.Clear();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (MarkaTextBox.Text is not null && ModelTextBox.Text is not null)
            {
                using (CarContext database = new())
                {

                    Car car = new()
                    {
                        Model = ModelTextBox.Text,
                        Marka = MarkaTextBox.Text,
                        Year = Convert.ToInt32(YearTextBox.Text),
                        StateNumber = Convert.ToInt32(StNumberTextBox.Text)
                    };

                    database.Cars.Add(car);

                    database.SaveChanges();

                    cars.Clear();

                    CarContext.Cars.ToList().ForEach(c => cars.Add(c));

                    ClearTextBox();
                }
            }
            else
            {
                MessageBox.Show("Enter Marka and Model");
                ClearTextBox();
            }
        }

        private void CarListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CarListView.SelectedItem is not null)
            {
                DeleteButton.IsEnabled = true;
                UpdateButton.IsEnabled = true;

                MarkaTextBox.Text = cars[CarListView.SelectedIndex].Marka;
                ModelTextBox.Text = cars[CarListView.SelectedIndex].Model;
                YearTextBox.Text = cars[CarListView.SelectedIndex].Year.ToString();
                StNumberTextBox.Text = cars[CarListView.SelectedIndex].StateNumber.ToString();
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (CarContext database = new())
            {
                Car car = database.Cars.FirstOrDefault(c => c.Id == cars[CarListView.SelectedIndex].Id)!;

                database.Remove(car);

                database.SaveChanges();

                ClearTextBox();

                CarListView.SelectedItem = null;

                cars.Clear();

                CarContext.Cars.ToList().ForEach(c => cars.Add(c));
            }
        }
    }
}
