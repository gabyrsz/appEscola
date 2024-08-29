namespace AppCrudEscola
{
    public class Escola
    {
        public int id { get; set; }
        public string nome { get; set; }
        public int idade { get; set; }
        public string curso { get; set; }
        public DateTime data_matricula { get; set; }


        public override string ToString()
        {
            return $"Id: {id}, Nome: {nome}, Idade: {idade}, Curso: {curso},  Data de matricula: {data_matricula}";
        }

    }
}
