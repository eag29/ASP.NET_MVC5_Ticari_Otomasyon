﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TicariOtomasyon.Models.Siniflar
{
    public class Cari
    {
        [Key]
        public int CariID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(25, ErrorMessage ="En fazla 25 karakter yazabilirsiniz!!!")]
        public string CariAd { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(25)]
        [Required(ErrorMessage ="Bu alan boş geçilemez!")]
        public string CariSoyad { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(13)]
        public string CariSehir { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string CariMail { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(15)]
        public string Sifre { get; set; }
        public ICollection<SatisHareket> SatisHarekets { get; set; }
    }
}