using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlightLogs.Models;
namespace FlightLogs.Controllers
{
    public class FlightsController : Controller
    {
        // GET: Flights

        DataSet ds = null;
        List<Model_Flights> blist = null;

        SqlConnection con = null;
        // GET: Flight
        public ActionResult Index()
        {        //
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
                SqlCommand cmd = new SqlCommand(@"SelectAllFlights", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                blist = new List<Model_Flights>();
                string boolval = "";
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int ctr = 0; ctr < ds.Tables[0].Rows.Count; ctr++)
                    {
                        Model_Flights ff = new Model_Flights();

                        ff.flight_id = Convert.ToInt32(ds.Tables[0].Rows[ctr]["flight_id"]);
                        ff.PilotName = Convert.ToString(ds.Tables[0].Rows[ctr]["pilotname"]);
                        ff.Schedule = Convert.ToDateTime(ds.Tables[0].Rows[ctr]["schedule"]);
                        ff.Origin = Convert.ToString(ds.Tables[0].Rows[ctr]["Origin"]);
                        ff.Destination = Convert.ToString(ds.Tables[0].Rows[ctr]["Destination"]);
                        ff.Flight1 = Convert.ToString(ds.Tables[0].Rows[ctr]["Flight"]);

                        blist.Add(ff);
                    }
                }
                else
                {
                    Model_Flights ff = new Model_Flights();

                    ff.flight_id = 0;
                    ff.PilotName = "";
                    ff.Schedule = DateTime.Now;
                    ff.Origin = "";
                    ff.Destination = "";
                }

            }
            catch
            {
                return null;

            }
            finally
            {
                con.Close();
            }
            return View(blist);
        }

        // GET: Flights/Details/5
        public ActionResult Details(int id)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
            Model_Flights eobj = new Model_Flights();

            SqlCommand cmd = new SqlCommand(@"SelectSpecificFlight", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds); List<Model_Flights> elist = null;
            elist = new List<Model_Flights>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                eobj.flight_id = id;
                eobj.PilotName = Convert.ToString(ds.Tables[0].Rows[i]["pilotname"]);
                eobj.Schedule = Convert.ToDateTime(ds.Tables[0].Rows[i]["schedule"]);
                eobj.Origin = Convert.ToString(ds.Tables[0].Rows[i]["Origin"]);
                eobj.Destination = Convert.ToString(ds.Tables[0].Rows[i]["Destination"]);
                eobj.Flight1 = Convert.ToString(ds.Tables[0].Rows[i]["Flight"]);

                elist.Add(eobj);
            }
            return View(eobj);
            //return View();
        }

        // GET: Flights/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Flights/Create
        [HttpPost]
        public ActionResult Create(Model_Flights createflight)
        {

            // TODO: Add insert logic here
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
            SqlCommand cmd = new SqlCommand(@"AddNewFlight", con);
            cmd.CommandType = CommandType.StoredProcedure;
            string result = "";
            //cmd.Parameters.AddWithValue("@CustomerID", 0);  
            cmd.Parameters.AddWithValue("@pilotname", createflight.PilotName);
            cmd.Parameters.AddWithValue("@flight", createflight.Flight1);
            cmd.Parameters.AddWithValue("@Schedule", createflight.Schedule);
            cmd.Parameters.AddWithValue("@Origin", createflight.Origin);
            cmd.Parameters.AddWithValue("@Destination", createflight.Destination);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return View();
        }

        // GET: Flights/Edit/5
        public ActionResult Edit(int id)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
            Model_Flights eobj = new Model_Flights();

            SqlCommand cmd = new SqlCommand(@"SelectSpecificFlight", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds); List<Model_Flights> elist = null;
            elist = new List<Model_Flights>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                eobj.flight_id = id;
                eobj.PilotName = Convert.ToString(ds.Tables[0].Rows[i]["pilotname"]);
                eobj.Schedule = Convert.ToDateTime(ds.Tables[0].Rows[i]["schedule"]);
                eobj.Origin = Convert.ToString(ds.Tables[0].Rows[i]["Origin"]);
                eobj.Destination = Convert.ToString(ds.Tables[0].Rows[i]["Destination"]);
                eobj.Flight1 = Convert.ToString(ds.Tables[0].Rows[i]["Flight"]);

                elist.Add(eobj);
            }
            return View(eobj);
        }

        // POST: Flights/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Model_Flights collection)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
                SqlCommand cmd = new SqlCommand(@"updateflight", con);
                cmd.CommandType = CommandType.StoredProcedure;
                string result = "";
                cmd.Parameters.AddWithValue("@id", id);

                cmd.Parameters.AddWithValue("@pilotname", collection.PilotName);
                cmd.Parameters.AddWithValue("@flight", collection.Flight1);
                cmd.Parameters.AddWithValue("@Schedule", collection.Schedule);
                cmd.Parameters.AddWithValue("@Origin", collection.Origin);
                cmd.Parameters.AddWithValue("@Destination", collection.Destination);

                //result =cmd.ExecuteScalar().ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //cmd.ExecuteScalar();         
                ViewBag.Message = "Branch Updated";
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: Flights/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Flights/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
