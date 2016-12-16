using Nancy;
using System.Collections.Generic;
using System;


namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/bands"] = _ =>{
        List<Band> allBands = Band.GetAll();
        return View["bands.cshtml", allBands];
      };
      Get["/band/new"] = _ => {
        return View["new_band.cshtml"];
      };
      Post["/band/new"] = _ => {
        Band newBand = new Band(Request.Form["band-name"]);
        newBand.Save();
        return View["new_band.cshtml", newBand];
      };
      Get["/venues"] = _ =>{
        List<Venue> allVenues = Venue.GetAll();
        return View["venues.cshtml", allVenues];
      };
      Get["/venue/new"] = _ => {
        return View["new_venue.cshtml"];
      };
      Post["/venue/new"] = _ => {
        Venue newVenue = new Venue(Request.Form["venue-name"]);
        newVenue.Save();
        return View["new_venue.cshtml", newVenue];
      };
      Get["/band/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Band foundBand = Band.Find(parameters.id);
        List<Venue> venues = foundBand.GetVenues();
        List<Venue> allVenues = Venue.GetAll();
        model.Add("current-band", foundBand);
        model.Add("venues-in-band", venues);
        model.Add("all-venues", allVenues);
        return View["band.cshtml", model];
      };
      Post["/band/add_venue"] = _ =>{
        Band band = Band.Find(Request.Form["band-id"]);
        Venue venue = Venue.Find(Request.Form["venue-id"]);
        band.AddVenue(venue);
        return View["success.cshtml"];
      };
      Get["/venue/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Venue foundVenue = Venue.Find(parameters.id);
        List<Band> bands = foundVenue.GetBands();
        List<Band> allBands = Band.GetAll();
        model.Add("current-venue", foundVenue);
        model.Add("bands-in-venue", bands);
        model.Add("all-bands", allBands);
        return View["venue.cshtml", model];
      };
      Post["/venue/add_band"] = _ => {
        Venue newVenue = Venue.Find(Request.Form["venue-id"]);
        Band newBand = Band.Find(Request.Form["band-id"]);
        newVenue.AddBand(newBand);
        return View["success.cshtml"];
      };
      Get["/venue/delete/{id}"] = parameters => {
        Venue newVenue = Venue.Find(parameters.id);
        return View["venue_delete.cshtml", newVenue];
      };
      Delete["/venue/delete/{id}"] = parameters => {
        Venue selectedVenue = Venue.Find(parameters.id);
        selectedVenue.Delete();
        return View["success.cshtml"];
      };
      Get["venue/edit/{id}"] = parameters => {
        Venue SelectedVenue = Venue.Find(parameters.id);
        return View["venue_edit.cshtml", SelectedVenue];
      };
      Patch["/venue/edit/{id}"] = parameters => {
      Venue SelectedVenue = Venue.Find(parameters.id);
      SelectedVenue.Update(Request.Form["venue-name"]);
      return View["success.cshtml"];
      };
    }
  }
}
