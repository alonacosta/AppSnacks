﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSnacks.Models
{
    public class ImageProfile
    {
        public string? UrlImage { get; set; }
        public string? PathImage => AppConfig.BaseUrl + UrlImage;

    }
}
