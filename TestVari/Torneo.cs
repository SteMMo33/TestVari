using RestSharp;
using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace TorneoSB
{
	public class Torneo
	{
		// I membri devono essere 'proprietà' per consentire il parsing JSON

		public string id { get; set; }		// Da query arriva una stringa
		public string nome { get; set; }
		public DateTime dataDa {  get; set; }
		public DateTime dataA { get; set; }


		/// <summary>
		/// Factory di conversione
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		internal static Torneo FromData( TorneoQuery query)
		{
			Torneo torneo = new Torneo();
			torneo.id = query.id;
			torneo.nome = query.nome;

			DateTime tmp;
			bool parsed = DateTime.TryParse(query.dataIniziale, out tmp);
			torneo.dataDa = parsed ? tmp : new DateTime(2020, 01, 01);
			parsed = DateTime.TryParse(query.dataFinale, out tmp);
			torneo.dataA = parsed ? tmp : new DateTime(2020, 01, 01);
			return torneo;
		}

	}


	public class TorneoQuery
	{
		public string id { get; set; }
		public string nome { get; set; }
		public string dataIniziale { get; set; }
		public string dataFinale { get; set; }
	}

	class TorneiQuery
	{
		public string ret { get; set; }
		public List<TorneoQuery> list { get; set; }
	}

}