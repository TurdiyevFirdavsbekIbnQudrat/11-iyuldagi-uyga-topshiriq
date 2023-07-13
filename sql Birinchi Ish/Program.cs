/*
using Npgsql;
namespace Database
{

    class data
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=Firdavs1;Database=dvdrental;";
            var con = new NpgsqlConnection(connectionString);
            con.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;
            var result = GetBySubject();
            Console.WriteLine();
            
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }

            IEnumerable<string> GetBySubject()
            {
                cmd.CommandText = $"Select * From person";
                NpgsqlDataReader reader = cmd.ExecuteReader();
                var result = new List<string>();
                while (reader.Read())
                {
                    result.Add(reader[1] as string);
                }
                return result;
            }

        }
    }
}
*/
using Dapper;
using Npgsql;
using System.Globalization;

DbFunctions FunksiyalardanFoydalanish = new DbFunctions();
//FunksiyalardanFoydalanish.getUser();
///FunksiyalardanFoydalanish.Add();
FunksiyalardanFoydalanish.Update(2,"Firdavs","",12);
//FunksiyalardanFoydalanish.Delete("Firdavs");
class User
{
    public int id { get; set; }
    public string firstName { get; set; }
    public string latname { get; set; }
    public int age { get; set; }
}


public class DbFunctions :  IDbFunctions
{

    public string connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=Firdavs1;Database=dvdrental;";
    
    public void getUser()
    {
        var con = new NpgsqlConnection(connectionString);
        string sql = "select * from public.User";
        con.Open();
        var users = con.Query<User>(sql);
        foreach (var item in users)
        {
            Console.WriteLine($"ism:{item.firstName} familiya:{item.latname} yosh:{item.age}");
        }

    }

    public void Add()
    {
       using(var con =new NpgsqlConnection(connectionString))
        {
            
            Console.Write("Bazaga nechta element qo'shmoqchisiz = ");
            int element_soni = Convert.ToInt32( Console.ReadLine());
          
            for(int i=0;i<element_soni;i++)
            {
                Console.Write("id = ");
                int id= Convert.ToInt32( Console.ReadLine());
                Console.Write("first name = ");
                string firstName = Console.ReadLine();
                Console.Write("Last Name = ");
                string latName = Console.ReadLine();
                Console.Write("age = ");
                int age = Convert.ToInt32(Console.ReadLine());
                //var sqlQuery = $"insert into public.User(id,firstname,latname,age) values('{id}','{firstName}','{latName}','{age}'";
                var a = new User()
                {
                    id = id,
                    firstName = firstName,
                    latname = latName,
                    age = age,
                };
                var sqlQuery = "insert into public.User values (@id,@firstname,@latname,@age)";
                con.Execute(sqlQuery,a);
            }
        }
    }
    
    public void Update(int Newid,string NewfirstName="",string Newlatname="",int Newage=-1)
    {   
        using (var con = new NpgsqlConnection(connectionString))
        {
            string sqlQuery = $"update public.User "
                + $"set public.User.firstName =(case when LENGTH('{NewfirstName}')!=0 THEN '{NewfirstName}' else  public.User.firstName end)," +
                $"public.User.latname=(case when LENGTH('{Newlatname}')!=0 THEN '{Newlatname}' else public.User.latname )," +
                $"public.User.age=(case when '{Newage}'>0 THEN '{Newage}' else public.User.age end)" +
                $" where public.User.id='{Newid}'";
            con.Execute(sqlQuery);
        }

    }
    
    public void Delete(string name)
    {
        using(var con = new NpgsqlConnection(connectionString))
        {
            string sqlQuery = $"delete from public.User where firstName='{name}' or latname='{name}'";
            con.Execute(sqlQuery);
        }
    }
    public void DeleteAll()
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            string sqlQuery = $"delete from public.User";
            con.Execute(sqlQuery);
        }
    }
    public void DeleteById(int user_id)
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            string sqlQuery = $"delete from public.User where public.User.id='{user_id}'";
            con.Execute(sqlQuery);
        }
    }
    public void GetById(int user_id)
    {
        using(var con =new NpgsqlConnection(connectionString))
        {
            string sqlQuery = $"insert into * from public.User where public.User.id='{user_id}'";
            con.Execute(sqlQuery);
        }
    }
}


interface IDbFunctions
{   
    void Add();
    void Update(int id,string firstName,string latname,int age);
    void Delete(string name);
    void DeleteAll();
    void DeleteById(int user_id);
    void GetById(int user_id);
    void getUser();

} 