namespace Kemia {
    class Felfedezes {

        public string Ev { get; private set; }
        public string Elem { get; private set; }
        public string Vegyjel { get; private set; }
        public int Rendszam { get; private set; }
        public string Felfedezo { get; private set; }

        public Felfedezes(string input) {
            string[] split = input.Split(';');

            Ev = split[0];
            Elem = split[1];
            Vegyjel = split[2];
            Rendszam = int.Parse(split[3]);
            Felfedezo = split[4];
        }
    }
}
