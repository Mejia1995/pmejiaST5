using pmejiaST5.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pmejiaST5
{
    public class PersonRepository

    {
       
        string _dbPath;
        private SQLiteConnection conn;

        public string StatusMessage { get; set; }

        private void Init()
        {
            if (conn is not null)
                return;
            conn = new(_dbPath);
            conn.CreateTable<Persona>();

        }
        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }


        public void AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                Init(); // verifica la conexion y crea la tabla
                if (string.IsNullOrEmpty(name))  // Verifica si el nombre ingresado no está vacío o nulo
                    throw new Exception("Nombre es requerido");
                Persona person = new() { Name = name };
                result = conn.Insert(person);  // Inserta la nueva persona en la base de datos y guarda el resultado
                StatusMessage = string.Format("Se inserto una persona", result, name);

            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Error al insertar persona", name, ex.Message);
            }

        }

        public List<Persona> GetAllPeople()
        {

            try
            {
                Init();// verifica la conexion y crea la tabla
                return conn.Table<Persona>().ToList();  // Obtiene todas las personas de la tabla 'Persona' en la base de datos y las convierte a una lista
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Error al Listar", ex.Message);
                return new List<Persona>();   // Devuelve una lista vacía en caso de error
            }
            
        }
        public void UpdatePerson(int id, string newName)
        {
            try
            {
                Init(); // verifica la conexion y crea la tabla
                var person = conn.Table<Persona>().FirstOrDefault(p => p.Id == id); // Busca la persona con el ID proporcionado en la base de datos
                if (person != null)  // Verifica si se encontró una persona con el ID proporcionado
                {
                    person.Name = newName;  // Actualiza el nombre de la persona con el nuevo nombre proporcionado
                    conn.Update(person);  // Actualiza la entrada de la persona en la base de datos
                    StatusMessage = string.Format("Persona actualizada. ID: {0}, Nuevo nombre: {1}", id, newName);
                }
                else
                {
                    StatusMessage = string.Format("No se encontró la persona con ID: {0}", id);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al actualizar persona. ID: {0}, Error: {1}", id, ex.Message);
            }
        }
        public void DeletePerson(int id)
        {
            try
            {
                Init(); // verifica la conexion y crea la tabla
                int result = conn.Table<Persona>().Delete(p => p.Id == id);   // Elimina la persona de la base de datos utilizando su ID
                if (result > 0)   // Verifica si se eliminó alguna fila (persona) de la base de datos
                {
                    StatusMessage = string.Format("Persona eliminada. ID: {0}", id);
                }
                else
                {
                    StatusMessage = string.Format("No se encontró la persona con ID: {0}", id);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al eliminar persona. ID: {0}, Error: {1}", id, ex.Message);
            }
        }
    }       

    }
