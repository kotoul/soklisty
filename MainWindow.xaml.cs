using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ListyEngine listyEngine;
        private StenyEngine stenyEngine;

        public MainWindow()
        {
            listyEngine = new ListyEngine(0);
            stenyEngine = new StenyEngine();
            InitializeComponent();
            pack();
            drawSteny();
        }

        private void pack()
        {
            listyLst.Items.Clear();

            listyEngine.Listy.Clear();
            listyEngine.pack(stenyEngine.Steny);
            listyEngine.Listy.ForEach(lista => listyLst.Items.Add(lista));

            int sum = stenyEngine.Steny.Select(s => s.Length).Sum();
            resLbl.Content = String.Format("Sum: {0}, needed at least: {1}, used: {2}", sum, (sum + listyEngine.ListaLen - 1) / listyEngine.ListaLen, listyEngine.Listy.Count);
        }

        private void drawSteny()
        {
            canvas.Children.Clear();
            stenyEngine.Steny.ForEach(stena => {
                Brush stroke = stena.getListy().Any(l => l.Selected) ? stena.Children != null ? Brushes.Pink : Brushes.Red : Brushes.Black;
                Line line = new Line() { X1 = stena.StartPoint.X, Y1 = stena.StartPoint.Y, X2 = stena.GetEndPoint().X, Y2 = stena.GetEndPoint().Y, 
                                         Stroke = stroke, StrokeThickness = stroke != Brushes.Black ? 8 : 2 };
                canvas.Children.Add(line);
            });
        }

        private int parseInt(string str)
        {
            try
            {
                return int.Parse(str);
            }
            catch (FormatException)
            {
                return 0;
            }
        }

        #region events

        private void packClick(object sender, RoutedEventArgs e)
        {
            pack();
        }

        private void lenTboxChanged(object sender, TextChangedEventArgs e)
        {
            listyEngine.ListaLen = parseInt(lenTbox.Text);
        }

        private void poleKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                pack();
            }
        }

        private void listyLstChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listyLst.SelectedItem != null)
            {
                Lista lista = listyLst.SelectedItem as Lista;
                listyEngine.Listy.ForEach(l => l.Selected = lista == l);
                drawSteny();
            }
        }

        #endregion
    }
}
