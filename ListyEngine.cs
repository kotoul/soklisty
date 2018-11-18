using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf
{
    public class Lista
    {
        public List<Stena> Steny { get; set; }
        public int UsedListaLen { get; set; }
        public bool Selected { get; set; }
        
        public Lista(Stena stena) {
            Selected = false;
            UsedListaLen = stena.Length;
            Steny = new List<Stena>() { stena };
            stena.Lista = this;
        }

        public void add(Stena stena) {
            UsedListaLen += stena.Length;
            Steny.Add(stena);
            stena.Lista = this;
        }

        public override string ToString()
        {
            return String.Format("{0} - [{1}]", UsedListaLen, String.Join(", ", Steny));
        }
    }

    public class ListyEngine
    {
        public List<Lista> Listy { get; set; }
        public int ListaLen { get; set; }

        public ListyEngine(int listaLen)
        {
            Listy = new List<Lista>();
            ListaLen = listaLen;
        }

        public List<Stena> pack(List<Stena> steny)
        {
            List<Stena> sorted = steny.SelectMany(s => s.Length > ListaLen ? s.split(ListaLen) : s.reset()).OrderByDescending(s => s.Length).ToList();

            sorted.ForEach(stena => {
                Lista fit = Listy.Find(lista => lista.UsedListaLen + stena.Length <= ListaLen);
                if (fit == null) {
                    Listy.Add(new Lista(stena));
                } else {
                    fit.add(stena);
                }
            });

            return sorted;
        }
    }
}
