using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testWebApplication.t4.Template
{
    public class TemplateEntity
    {

        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空！")]
        [MaxLength(50, ErrorMessage = "长度不能大于50！")]
        public string name { get; set; }

        public string code { get; set; }
    }
}