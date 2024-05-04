namespace pmejiaST5
{
    public partial class App : Application
    {
        public static PersonRepository personRepo { get; set; }
        public App(PersonRepository personRepository)
        {
            InitializeComponent();

            MainPage = new Vistas.vPersona();
            personRepo = personRepository;
        }
    }
}
