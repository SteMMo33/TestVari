using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TorneoSB;

namespace TestVari
{
	/// <summary>
	/// Logica di interazione per MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		static HttpStatusCode statusCode;
		public static HttpStatusCode StatusCode { get { return statusCode; } }


		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			List<Torneo> tornei = GetTornei();
			Console.WriteLine($"Trovati {tornei.Count} tornei");
		}


		/// <summary>
		/// Inizializzazione valori comuni alle chiamate REST
		/// </summary>
		/// <returns></returns>
		static private RestClient InitRest()
		{
			var options = new RestClientOptions("https://www.salabaganzabc.com/Torneo/1.1/")
			{
				Authenticator = new HttpBasicAuthenticator("username", "password"),
				ThrowOnDeserializationError = true,
				FailOnDeserializationError = true,
			};
			return new RestClient(options);
		}



		/// <summary>
		/// Recupera la lista dei tornei in DB
		/// </summary>
		static public List<Torneo> GetTornei()
		{
			var client = InitRest();
			var request = new RestRequest("GetTornei.php", Method.Get);

			List<Torneo> list = new List<Torneo>();
			try
			{
				var response = client.ExecuteGet<TorneiQuery>(request);
				Debug.WriteLine(response.Content);
				Debug.WriteLine(response.StatusCode);
				if (response.StatusCode == HttpStatusCode.OK)
				{
					if (response.Data != null)
						foreach (var item in response.Data.list)
						{
							Torneo t = Torneo.FromData(item);
							list.Add(t);
						};
				}

				statusCode = response.StatusCode;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
			return list;
		}



	}
}
