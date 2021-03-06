﻿// Itinero - OpenStreetMap (OSM) SDK
// Copyright (C) 2015 Abelshausen Ben
// 
// This file is part of Itinero.
// 
// Itinero is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// Itinero is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Itinero. If not, see <http://www.gnu.org/licenses/>.

using Nancy;
using Nancy.Json;
using System;
using System.IO;

namespace Itinero.Logistics.API.Reponses
{
    /// <summary>
    /// A GeoJSON response.
    /// </summary>
    public class GeoJsonResponse : Response
    {
        /// <summary>
        /// Holds the default content type.
        /// </summary>
        private static string DefaultContentType
        {
            get
            {
                return "application/json" + "; charset=" + JsonSettings.DefaultEncoding.EncodingName;
            }
        }

        /// <summary>
        /// Creates a new GeoJSON response.
        /// </summary>
        public GeoJsonResponse(Route model)
        {
            this.Contents = model == null ? NoBody : GetGeoJsonContents(model);
            this.ContentType = DefaultContentType;
            this.StatusCode = HttpStatusCode.OK;
        }

        /// <summary>
        /// Gets a stream for a given feature collection.
        /// </summary>
        /// <returns></returns>
        private static Action<Stream> GetGeoJsonContents(Route model)
        {
            return stream =>
            {
                var geoJson = model.ToGeoJson();

                var geoJsonBytes = System.Text.Encoding.UTF8.GetBytes(geoJson);
                stream.Write(geoJsonBytes, 0, geoJsonBytes.Length);
            };
        }
    }
}
