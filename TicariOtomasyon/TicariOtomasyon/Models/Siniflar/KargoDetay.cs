﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TicariOtomasyon.Models.Siniflar
{
    public class KargoDetay
    {
        [Key]
        public int KargoDetayID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(300)]
        public string Aciklama { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string TakipKodu { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(25)]
        public string Personel { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(25)]
        public string Alici { get; set; }
        public DateTime Tarih { get; set; }
    }
}