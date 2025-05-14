using System;
using System.Collections.Generic;
using Mobappg4v2.ViewModel;
using static Mobappg4v2.Model.TrackOrderModel;

namespace Mobappg4v2.Model
{
    public class TrackOrderModel
    {
        public string OrderDate { get; set; }
        public List<Track> TrackList { get; set; }

        public TrackOrderModel(string orderDate, List<Track> trackList)
        {
            OrderDate = orderDate;
            TrackList = trackList;
        }

        public class Track
        {
            public string OrderId { get; set; }
            public string Price { get; set; }
            public string Status { get; set; }
            public List<ImageList> Images { get; set; }

            public Track()
            {
                Images = new List<ImageList>();
            }
        }

        public class ImageList
        {
            public string ImageUrl { get; set; }
        }
    }
}