using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Govornik
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		List<Slusaoc> Slusaoci = new List<Slusaoc>();
		public MainWindow()
		{
			InitializeComponent();
			Slusaoci.Add(new Slusaoc("Pera", "Peric"));
			Slusaoci.Add(new Slusaoc("Neko", "Nekic"));
			Slusaoci.Add(new Slusaoc("Trecko", "Treckovic"));
			dg.ItemsSource = Slusaoci;

			DataContext = new Predavac();

			foreach (Slusaoc s in Slusaoci)
				(DataContext as Predavac).GovorSeDogadja += s.Slusam;

		}

		private void Klik(object sender, RoutedEventArgs e)
		{
			(DataContext as Predavac).Govori();
			dg.ItemsSource = Slusaoci.ToList();
		}
	}

	public class GovorEventArgs : EventArgs
	{
		public string Govor;
	}

	public class Predavac
	{
		public delegate void GovorDelegat(object KoSalje, GovorEventArgs g);

		public event GovorDelegat GovorSeDogadja;

		public string Govor { get; set; }
		public void Govori()
		{
			GovorEventArgs g = new GovorEventArgs();
			g.Govor = this.Govor;
			GovorSeDogadja?.Invoke(this, g);
		}
		
	}
	public class Slusaoc
	{
		public string Ime { get; set; }
		public string Prezime { get; set; }
		public string ZadnjeReceno { get; set; }

		public void Slusam(object Govornik, GovorEventArgs g) => ZadnjeReceno = g.Govor;

		public Slusaoc(string i, string p)
		{
			Ime = i;
			Prezime = p;
		}
	}
}
