using System.Text.RegularExpressions;
using CMS.Helpers;

namespace ChangeProperty
{
	static class ChangeProperty
	{
		/// <summary>
		/// Takes the given json (Template or Widgets) and replaces NodeAliasPath of the given property with the proper format for the KX13 PathSelector.  
		/// 
		/// NOTE: This will change ANY value of the matching given propertyName, please make sure you do not have other widgets/template properties that share the same property name but are not going to be a Path Selector
		/// </summary>
		/// <param name="json">The Template or Widget json (From ProcessTemplateWidgetJson event args)</param>
		/// <param name="propertyName">The property name (Case sensitive) that will be changed, i.e., ItemID</param>
		/// <param name="newPropertyName">The property name to change to, i.e., ItemGUID</param>
		/// <returns>The Json string with transformed property</returns>
		public static string TransformIdentifer(string json, string propertyName, string newPropertyName)
		{
			var regex = RegexHelper.GetRegex($"\"{propertyName}\":\"([~\\/_a-zA-Z0-9-.\\%]*)\",");
			
			var matches = regex.Matches(json);
			if (matches.Count > 0)
			{
				foreach (Match match in matches)
				{
					if (match.Groups.Count > 0)
					{
						var group = match.Groups[1];
						json = json.Replace($"\"{propertyName}\":\"{group.Value}\",", $"\"{newPropertyName}\":,");
					}
				}
			}
			return json;
		}
	}
}
