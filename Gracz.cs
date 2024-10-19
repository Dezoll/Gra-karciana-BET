using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrawBETA
{
	internal class Gracz
	{
		public Gracz() { }

		private List<string> karty = new List<string>();
		private int Lewe = 0;
		private float kasa = 0.0f;
		public void Karty(string Karta1, string Karta2, string Karta3)
		{
			karty.Clear(); // Usunięcie poprzednich kart, jeśli istniały
			karty.Add(Karta1);
			karty.Add(Karta2);
			karty.Add(Karta3);
		}
		public void LewawRundzie()
		{
			Lewe++;
		}
		public List<string> getKarty()
		{
			return karty;
		}

		public void zerujLewe()
		{
			Lewe = 0;
		}

		public int getLewe()
		{
			return Lewe;
		}
		public void pokazLewe()
		{
            Console.WriteLine(Lewe);
        }
		public void aktualizujKarty(List<string> karty1)
		{
			karty = karty1;
		}

		public void pokazKarty()
		{
			
				Console.WriteLine(string.Join(" || " , karty));
		}

		


	}
}
