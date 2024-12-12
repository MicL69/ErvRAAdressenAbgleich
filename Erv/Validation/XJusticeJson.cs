using Newtonsoft.Json;
using System;
using System.Collections.Generic;
// ReSharper disable StringLiteralTypo

namespace Erv.Validation
{
    internal class XJusticeJson
    {
        [JsonProperty("daten")]
        [JsonConverter(typeof(XJusticeDataToDictionaryConverter))]
        public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();

        [JsonProperty("metadaten")] public Metadata Metadata { get; set; } = new Metadata();

        [JsonProperty("spalten")]
        public List<Column> Columns { get; set; } = new List<Column>();
    }

    internal class Metadata
    {
        [JsonProperty("aenderungZurVorversion")]
        public List<Change> Changes { get; set; } = new List<Change>();

        [JsonProperty("beschreibung")]
        public List<Description> Description { get; set; } = new List<Description>();

        [JsonProperty("gueltigAb")]
        public DateTime ValidFrom { get; set; }

        [JsonProperty("handbuchVersion")]
        public string HandbookVersion { get; set; } = string.Empty;

        [JsonProperty("herausgebernameKurz")]
        public List<Name> PublisherShortName { get; set; } = new List<Name>();

        [JsonProperty("herausgebernameLang")]
        public List<Name> PublisherLongName { get; set; } = new List<Name>();

        [JsonProperty("kennung")]
        public string Identifier { get; set; } = string.Empty;

        [JsonProperty("kennungInhalt")]
        public string ContentIdentifier { get; set; } = string.Empty;

        [JsonProperty("nameKurz")]
        public List<Name> ShortName { get; set; } = new List<Name>();

        [JsonProperty("nameLang")]
        public List<Name> LongName { get; set; } = new List<Name>();

        [JsonProperty("nameTechnisch")]
        public string TechnicalName { get; set; } = string.Empty;

        [JsonProperty("version")]
        public string Version { get; set; } = string.Empty;

        [JsonProperty("versionBeschreibung")]
        public List<Description> VersionDescription { get; set; } = new List<Description>();

        [JsonProperty("xoevHandbuch")]
        public bool XoevHandbook { get; set; }
    }

    internal class Change
    {
        public string Value { get; set; } = string.Empty;
    }

    internal class Description
    {
        public string Value { get; set; } = string.Empty;
    }

    internal class Name
    {
        public string Value { get; set; } = string.Empty;
    }

    internal class Column
    {
        [JsonProperty("codeSpalte")]
        public bool IsCodeColumn { get; set; }

        [JsonProperty("datentyp")]
        public string DataType { get; set; } = string.Empty;

        [JsonProperty("empfohleneCodeSpalte")]
        public bool IsRecommendedCodeColumn { get; set; }

        [JsonProperty("spaltennameLang")]
        public string ColumnLongName { get; set; } = string.Empty;

        [JsonProperty("spaltennameTechnisch")]
        public string ColumnTechnicalName { get; set; } = string.Empty;

        [JsonProperty("verwendung")]
        public string Usage { get; set; } = string.Empty;
    }
}
