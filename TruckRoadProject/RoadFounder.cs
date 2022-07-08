namespace TruckRoadProject
{
    public class RoadFounder
    {
        private static Random random = new Random();
        private static int _wielkoscPopulacji = 20;
        private static int _liczbaIteracji = 100000;
        private static int _prawdopodobienstwoKrzyzowania = 95;
        private static int _prawdopodobienstwoMutacji = 5;
        private static int _min = int.MaxValue;
        private static int _indeksSciezki;
        private static int _tmpMin, _tmpIndeksSciezki;
        
        private static int[,] _utworzonaMacierz; 
        private static int[,] _tabPopulacji; 
        private static int[,] _tabOdleglosci; 
        private static int[] _tabOcen;

        public List<int> Droga;
        

        public void Main()
        {
            TworzenieMacierzy(Environment.CurrentDirectory + @"\trasa.txt");
            TworzenieTabPopulacji();

            for (var i = 0; i < _liczbaIteracji; i++)
            {
                TworzenieTabOdleglosci(_tabPopulacji);
                TworzenieTabOcen(_tabOdleglosci);
                
                _tmpIndeksSciezki = 0;
                _tmpMin = _tabOcen[0];

                for (var j = 0; j < _tabOcen.GetLength(0); j++)
                {
                    if (_tabOcen[j] < _tmpMin)
                    {
                        _tmpMin = _tabOcen[j];
                        _tmpIndeksSciezki = j;
                    }
                }
                if (_tmpMin < _min)
                {
                    _min = _tmpMin;
                    _indeksSciezki = _tmpIndeksSciezki;
                     Droga = new List<int>();
                    for (var j = 0; j < _tabPopulacji.GetLength(1); j++)
                    {
                       
                        Console.Write(_tabPopulacji[_indeksSciezki, j] + "-");
                        Droga.Add(_tabPopulacji[_indeksSciezki, j]);
                    }

                    Console.WriteLine(_min);
                }
                
                for (var j = 0; j < _tabPopulacji.GetLength(0); j++)
                {
                    var x = SelekcjaTurniejowa(_tabOcen);
                    for (var k = 0; k < _tabPopulacji.GetLength(1); k++)
                        _tabPopulacji[j, k] = _tabPopulacji[x, k];

                }

                //KRZYŻOWANIE
                if (random.Next(1, 101) <= _prawdopodobienstwoKrzyzowania) KrzyzowaniePMX(_tabPopulacji);

                //MUTACJA
                MutacjaPrzezInwersje(_tabPopulacji);

            }
        }

        public static void TworzenieMacierzy(string sciezkaPliku)
        {
            int liczbaWierzcholkow;
            var i = 0;
            int j;
            var czyPierwszaLinia = true;

            try
            {
                var tekst = File.ReadLines(sciezkaPliku);
                foreach (var linia in tekst)
                {
                    if (czyPierwszaLinia)
                    {
                        if (int.TryParse(linia, out liczbaWierzcholkow))
                        {
                            _utworzonaMacierz = new int[liczbaWierzcholkow, liczbaWierzcholkow];
                            _tabPopulacji = new int[_wielkoscPopulacji, liczbaWierzcholkow];
                            _tabOdleglosci = new int[_wielkoscPopulacji, liczbaWierzcholkow];
                            _tabOcen = new int[_wielkoscPopulacji];
                        }
                        czyPierwszaLinia = false;
                    }
                    else
                    {
                        j = 0;
                        foreach (string liczba in linia.Split(' '))
                        {
                            if (liczba != "" && liczba != " ")
                            {
                                _utworzonaMacierz[i, j] = int.Parse(liczba);
                                _utworzonaMacierz[j, i] = int.Parse(liczba);
                                j++;
                            }
                        }
                        i++;
                    }
                }
            }
            catch (InvalidCastException e) when (e.Data != null)
            {
                Console.WriteLine("ERROR");
            }
        }

        private static void TworzenieTabPopulacji()
        {
            int x, z;
            var liczby = new int[_tabPopulacji.GetLength(1)];

            for (var i = 0; i < _tabPopulacji.GetLength(0); i++)
            {
                z = _tabPopulacji.GetLength(1);
                for (var j = 0; j < z; j++)
                    liczby[j] = j;

                for (var j = 0; j < _tabPopulacji.GetLength(1); j++)
                {
                    x = random.Next(z);
                    _tabPopulacji[i, j] = liczby[x];
                    liczby[x] = liczby[z - 1];
                    z--;
                }
            }
        }

        public static void TworzenieTabOdleglosci(int[,] tab)
        {
            for (var i = 0; i < _tabOdleglosci.GetLength(0); i++)
            {
                for (var j = 0; j < _tabOdleglosci.GetLength(1); j++)
                {
                    int tmp1;
                    int tmp2;
                    if (j != _tabOdleglosci.GetLength(1) - 1)
                    {
                        tmp1 = tab[i, j];
                        tmp2 = tab[i, j + 1];
                        _tabOdleglosci[i, j] = _utworzonaMacierz[tmp1, tmp2];
                    }
                    else
                    {
                        tmp1 = tab[i, j];
                        tmp2 = tab[i, 0];
                        _tabOdleglosci[i, j] = _utworzonaMacierz[tmp1, tmp2];
                    }
                }
            }
        }

        public static void TworzenieTabOcen(int[,] tab)
        {
            for (var i = 0; i < _tabOdleglosci.GetLength(0); i++)
            {
                _tabOcen[i] = 0;
                for (var j = 0; j < _tabOdleglosci.GetLength(1); j++)
                    _tabOcen[i] += tab[i, j];
            }
        }

        public static int SelekcjaTurniejowa(int[] tab)
        {
            var k = 3;
            var tmp = random.Next(0, tab.Length);
            var naj = tab[tmp];
            var indeks = tmp;

            for (var i = 1; i < k - 1; i++)
            {
                tmp = random.Next(0, tab.Length);
                var nowy = tab[tmp];

                if (nowy < naj)
                {
                    naj = nowy;
                    indeks = tmp;
                }
            }
            return indeks;
        }

        public static int SelekcjaKoloRuletki(int[] tab, int[,] tabPop)
        {
            var tabWycinkowKola = new double[tab.Length]; // jaki osobnik posiada jaką część koła
            var indeks = 0;

            var suma = tab.Sum();

            for (var i = 0; i < tab.Length; i++)
            {
                if (i > 0) tabWycinkowKola[i] = Math.Round(tabWycinkowKola[i - 1] + ((double)tab[i] / suma * 100), 2);
                else tabWycinkowKola[i] = Math.Round((double)tab[i] / suma * 100, 2);
            }

            double tmp = random.Next(1, 101);
            var p1 = 0d;
            var p2 = tabWycinkowKola[0]; // przedziały

            for (var i = 0; i < tab.Length; i++)
            {
                if (tmp > p1 && tmp <= p2)
                {
                    indeks = i;
                    break;
                }

                p1 += p2;
                p2 = tabWycinkowKola[i + 1];
            }
            return indeks;
        }

        public static void KrzyzowaniePMX(int[,] tab)
        {
            var pot = new int[tab.GetLength(1)];
            var pot2 = new int[tab.GetLength(1)];


            for (var i = 0; i < tab.GetLength(0); i += 2)
            {
                var p1 = random.Next(1, tab.GetLength(1) - 2);
                var p2 = random.Next(p1 + 1, tab.GetLength(1) - 1);

                for (var j = p1; j <= p2; j++)
                {
                    pot[j] = tab[i + 1, j];
                    pot2[j] = tab[i, j];
                }

                Przepisz(pot, tab, i, 0, p1, p2);
                Przepisz(pot, tab, i, p2 + 1, p1, p2);
                Przepisz(pot2, tab, i + 1, 0, p1, p2);
                Przepisz(pot2, tab, i + 1, p2 + 1, p1, p2);

                for (var k = 0; k < pot.GetLength(0); k++)
                {
                    _tabPopulacji[i, k] = pot[k];
                    _tabPopulacji[i + 1, k] = pot2[k];
                }
            }
        }

        public static void MutacjaPrzezInwersje(int[,] tab)
        {
            var tabTmp = new int[tab.GetLength(1)];

            for (var i = 0; i < tab.GetLength(0); i++)
            {
                if (random.Next(1, 101) <= _prawdopodobienstwoMutacji)
                {
                    for (var j = 0; j < tab.GetLength(1); j++) tabTmp[j] = tab[i, j];

                    var p1 = random.Next(0, tab.GetLength(1) - 2);
                    var p2 = random.Next(p1 + 1, tab.GetLength(1) - 1);

                    var tabTmpOdwrocona = new int[p2 - p1 + 1];
                    var n = p2;
                    for (var j = p2; j >= p1; j--)
                    {
                        tabTmpOdwrocona[j - n] = tabTmp[j];
                        n -= 2;
                    }

                    var m = 0;
                    for (var j = p1; j <= p2; j++)
                    {
                        tabTmp[j] = tabTmpOdwrocona[m];
                        m++;
                    }

                    for (var j = 0; j < tab.GetLength(1); j++) tab[i, j] = tabTmp[j];

                }
            }
        }

        public static int Znajdz(int[] tab, int el, int start, int koniec)
        {
            for (var i = start; i <= koniec; i++)
            {
                if (tab[i] == el)
                {
                    return i;
                }
            }
            return -1;
        }

        public static void Przepisz(int[] pot, int[,] tab, int i, int pocz, int p1, int p2)
        {
            var koniec = p1;
            if (pocz > p2) koniec = pot.Length;
            for (var j = pocz; j < koniec; j++)
            {
                var gen = tab[i, j];
                var poz = -1;
                while ((poz = Znajdz(pot, gen, p1, p2)) > -1)
                {
                    gen = tab[i, poz];
                }
                pot[j] = gen;
            }
        }
        
    }
}

