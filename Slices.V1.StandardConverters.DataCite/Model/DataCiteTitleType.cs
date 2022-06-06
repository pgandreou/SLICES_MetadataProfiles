﻿using System.Xml.Serialization;

namespace Slices.V1.StandardConverters.DataCite.Model;

[Serializable]
[XmlType("titleType", Namespace = "http://datacite.org/schema/kernel-4")]
public enum DataCiteTitleType
{
    AlternativeTitle,

    Subtitle,

    TranslatedTitle,

    Other,
}
