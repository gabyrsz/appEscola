using MySql.Data.MySqlClient;

namespace AppCrudEscola
{
    public class Program
    {
        private static string connectionString = "Server=localhost;Database=db_aulas_2024;Uid=gaby;Pwd=1234567;SslMode=none;";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1 - Matricular aluno");
                Console.WriteLine("2 - Listar os alunos matriculados");
                Console.WriteLine("3 - Atualizar matrícula");
                Console.WriteLine("4 - Sair");
                Console.Write("Escolha uma opção acima:");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Adicionar();
                        break;
                    case "2":
                        Listar();
                        break;
                    case "3":
                        Atualizar();
                        break;
                    case "4":
                        Console.WriteLine(4);
                        return;

                    default:
                        Console.WriteLine("Opção Inválida!");
                        break;
                }
            }
        }
        static void Adicionar()
        {
            Console.Write("Informe o nome do aluno:");
            string nome = Console.ReadLine();
            Console.Write("Informe a idade do aluno:");
            int idade = int.Parse(Console.ReadLine());
            Console.Write("Informe o curso do aluno:");
            string curso = Console.ReadLine();
            Console.Write("Informe a data de matrícula do aluno:");
            DateTime data_matricula = DateTime.Parse(Console.ReadLine());

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO tb_alunos (nome,idade,curso,data_matricula) VALUES(@nome,@idade,@curso,@data_matricula)";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@idade", idade);
                cmd.Parameters.AddWithValue("@curso", curso);
                cmd.Parameters.AddWithValue("@data_matricula", data_matricula);
                cmd.ExecuteNonQuery();
            }
            Console.Write("Aluno matriculado com sucesso!\n");
        }

        static void Listar()
        {
            Console.Write("Informe o curso que deseja listar os alunos matriculados:");
            string cursoprocurar = Console.ReadLine();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id, nome, idade, curso, data_matricula from tb_alunos WHERE curso = @cursoprocurar";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@cursoprocurar", cursoprocurar);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Id: {reader["id"]}, Nome: {reader["nome"]}, Idade: {reader["idade"]}, Curso: {reader["curso"]}, Data de matricula: {reader["data_matricula"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Esse aluno não esta matriculado.");
                    }
                }
            }
        }
        static void Atualizar()
        {
            Console.WriteLine("Informe o Id do aluno que deseja atualizar a matricula:");
            int idEditar = int.Parse(Console.ReadLine());

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM tb_alunos WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", idEditar);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.Write("Informe a data de matrícula atualizada do aluno:");
                        DateTime nova_matricula = DateTime.Parse(Console.ReadLine());

                        reader.Close();

                        string queryUpdate = "UPDATE tb_alunos SET data_matricula = @data_matricula WHERE Id = @id";
                        cmd = new MySqlCommand(queryUpdate, connection);
                        cmd.Parameters.AddWithValue("@data_matricula", nova_matricula);
                        cmd.Parameters.AddWithValue("@id", idEditar);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Matrícula atualizada com sucesso.");
                    }
                    else
                    {
                        Console.WriteLine("O Id informado não existe.");
                    }
                }
            }
        }
    }
}