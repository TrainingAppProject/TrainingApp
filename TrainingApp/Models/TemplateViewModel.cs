/// <summary>
/// Module purpose: Template view model associated to TemplateDTO
/// Authors: Hansol Lee / Jei Yang
/// Date: Nov 6, 2022
/// Source: Created for the COMP7022 project
/// Revision History:
///
/// </summary>
using System;
using TrainingApp.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Collections.ObjectModel;

namespace TrainingApp.Models
{
    public class TemplateViewModel
    {
        public IEnumerable<TemplateDTO> Templates { get; set; }
        public TemplateDTO Template { get; set; }

    }
    public static class EnumProcess
    {
        public static string GetDisplayName(this Enum value)
        {
            Type enumType = value.GetType();
            var enumValue = Enum.GetName(enumType, value);
            MemberInfo member = enumType.GetMember(enumValue)[0];

            var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            var outString = ((DisplayAttribute)attrs[0]).Name;

            if (((DisplayAttribute)attrs[0]).ResourceType != null)
            {
                outString = ((DisplayAttribute)attrs[0]).GetName();
            }

            return outString;
        }
    }

}