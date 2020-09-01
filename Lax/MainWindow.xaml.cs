#region Overhead Libs
/*
 * IMPORTED OVERHEAD LIBRARIES
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Lax.Classes;
#endregion

#region Lax Namespace
namespace Lax
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    #region MainWindow
    public partial class MainWindow : Window
    {
        #region Properties
        /*OBSERVABLE COLLECTION PROPERTY
         * USED FOR REALTIME UPDATING LISTBOX ITEMS
         */
        private ObservableCollection<Movie> _movies = new ObservableCollection<Movie>();
        public ObservableCollection<Movie> movies { get { return _movies; } set { _movies = value; } }
        #endregion

        #region Constructor
        /*CONSTRUCTOR
         * Here we fill our 'movies' list upon startup of the program via our SQLToolbox
         */
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            foreach (var movie in SQLToolbox.GetMovieData())
            {
                movies.Add(movie);
            };
        }
        #endregion

        #region Events
        /*EVENTS
         *AUTO-GENERATED EVENTS
         */
        #region Delete Event
        /*
         * Pulls the item from the SelectedItem property inside of listbox
         * then removes the item from the db & then also the listbox
         */
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var item = listbox.SelectedItem as Movie;
            SQLToolbox.RemoveMovie(item);
            movies.Remove(item);
        }
        #endregion

        #region Create Event
        /*
         * Creates a new Movie with the values inside of the textboxes 
         * then adds the item to the db & then also the listbox
         */
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Movie newItem = new Movie();
            newItem.Thumbnail = Thumbnail.Text;
            newItem.Title = title.Text;
            newItem.Rating = double.Parse(Rating.Text.Replace('.', ','));
            newItem.Genre = Genre.Text;
            newItem.Runtime = int.Parse(RunTime.Text);
            newItem.Price = double.Parse(Price.Text.Replace('.', ','));
            newItem.ReleaseDate = ReleaseDate.Text;
            SQLToolbox.CreateMovie(newItem);
            movies.Add(newItem);
        }
        #endregion

        #region Save Event
        /*
         * Creates a new Movie with the values inside of the textboxes 
         * then adds the item to the db & then also the listbox
         */
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var item = listbox.SelectedItem as Movie;
            item.Thumbnail = Thumbnail.Text;
            item.Title = title.Text;
            item.Rating = double.Parse(Rating.Text.Replace('.', ','));
            item.Genre = Genre.Text;
            item.Runtime = int.Parse(RunTime.Text);
            item.Price = double.Parse(Price.Text.Replace('.',','));
            item.ReleaseDate = ReleaseDate.Text;
            SQLToolbox.UpdateMovie(item);
            movies.Remove(movies.FirstOrDefault(x => x.Id == item.Id));
            movies.Add(item);
        }
        #endregion

        #region Exit Event
        /*
         * Closes the MainWindow
         */
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #endregion
    }
    #endregion
}
#endregion