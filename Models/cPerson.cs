using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MySql.Data.MySqlClient;
using System.Configuration;

namespace Razor.Models
{
    public class cPerson
    {
        public int PersonID { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public byte[] Photo { get; set; }

        private bool connection_open;
        private MySqlConnection connection;

        public cPerson()
        {

        }

        public cPerson(int arg_id)
        {
            Get_Connection();
            PersonID = arg_id;
            //    m_Person = new CPersonMaster();
            //  List<CPersonMaster> PersonList = new List<CPersonMaster>();
            //PersonList = CComs_PM.Fetch_PersonMaster(connection, 4, arg_id);

            //if (PersonList.Count==0)
            //  return "";

            //m_Person = PersonList[0];

            //DB_Connect.CloseTheConnection(connection);
	try
	{
        

		MySqlCommand cmd = new MySqlCommand();
		cmd.Connection = connection;
//		cmd.CommandType = CommandType .Text;
		cmd.CommandText =
	string.Format("select concat (person_id, ') ', surname, ', ', forename) Person, Address1, Address2, photo, length(photo) from PersonMaster where Person_ID = '{0}'",
									  PersonID);

		MySqlDataReader reader = cmd.ExecuteReader();

		try
		{
			reader.Read();

			if (reader.IsDBNull(0) == false)
				Name = reader.GetString(0); 
			else
				Name = null;

            if (reader.IsDBNull(1) == false)
				Address1 = reader.GetString(1); 
			else
				Address1 = null;

            if (reader.IsDBNull(2) == false)
				Address2 = reader.GetString(2); 
			else
				Address2 = null;

			if (reader.IsDBNull(3) == false)
					{
						Photo = new byte[reader.GetInt32(4)];
                        reader.GetBytes(3, 0, Photo, 0, reader.GetInt32(4));
					}
					else
					{	
						Photo = null;
					}
            reader.Close();

		}
		catch (MySqlException e)
		{
			string  MessageString = "Read error occurred  / entry not found loading the Column details: "
				+ e.ErrorCode + " - " + e.Message + "; \n\nPlease Continue";
			//MessageBox.Show(MessageString, "SQL Read Error");
			reader.Close();
			Name= MessageString;
            Address1 = Address2 = null;
		}
	}
	catch (MySqlException e)
	{
			string  MessageString = "The following error occurred loading the Column details: "
				+ e.ErrorCode + " - " + e.Message;
			Name= MessageString;
            Address1 = Address2 = null;
		}
             
             
             
             
             connection.Close();


        }

        private void Get_Connection()
        {
            connection_open = false;

            connection = new MySqlConnection();
            //connection = DB_Connect.Make_Connnection(ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;

            //            if (db_manage_connnection.DB_Connect.OpenTheConnection(connection))
            if (Open_Local_Connection())
            {
                connection_open = true;
            }
            else
            {
                //					MessageBox::Show("No database connection connection made...\n Exiting now", "Database Connection Error");
                //					 Application::Exit();
            }

        }

        private bool Open_Local_Connection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}