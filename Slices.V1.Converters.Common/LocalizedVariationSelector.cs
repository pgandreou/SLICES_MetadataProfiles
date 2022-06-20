using System.Diagnostics.CodeAnalysis;

namespace Slices.V1.Converters.Common;

public class LocalizedVariationSelector
{
    private struct LangAnnotated<TItem>
    {
        public string? Language;
        public TItem Item;
    }

    public string PriorityLanguage { get; set; } = "en";

    public string? ResourceLang { get; set; }

    public Func<string, string>? GeneralizeLanguageCallback { get; set; }

    public bool PickBest<TItem>(
        IEnumerable<TItem> items,
        Func<TItem, string> langSelector,
        [NotNullWhen(true)] out TItem? selectedItem
    )
        where TItem : class
    {
        selectedItem = PickBest(items, langSelector).FirstOrDefault();

        return selectedItem != null;
    }

    [SuppressMessage(
        "ReSharper", "PossibleMultipleEnumeration",
        Justification = "Any() is cheap and we operate on in memory arrays"
    )]
    public IEnumerable<TItem> PickBest<TItem>(
        IEnumerable<TItem> items,
        Func<TItem, string> langSelector
    )
        where TItem : class
    {
        if (!items.Any()) return Array.Empty<TItem>();

        LangAnnotated<TItem>[] langAnnotatedItems = DeriveItemLanguages(items, langSelector).ToArray();

        // Prefer English
        IEnumerable<LangAnnotated<TItem>> selectedAnnotated = langAnnotatedItems
            .Where(t => t.Language == PriorityLanguage);

        // Otherwise prefer the resource lang
        if (!selectedAnnotated.Any())
        {
            string? resourceLang = GeneralizeLanguage(ResourceLang);

            if (!string.IsNullOrWhiteSpace(resourceLang))
            {
                selectedAnnotated = langAnnotatedItems.Where(t => t.Language == resourceLang);
            }
        }

        // Otherwise try to find something that has a lang set
        if (!selectedAnnotated.Any())
        {
            selectedAnnotated = langAnnotatedItems.Where(t => !string.IsNullOrWhiteSpace(t.Language));
        }

        // Otherwise just pick all
        if (!selectedAnnotated.Any())
        {
            selectedAnnotated = langAnnotatedItems;
        }

        return selectedAnnotated.Select(t => t.Item);
    }

    private IEnumerable<LangAnnotated<TItem>> DeriveItemLanguages<TItem>(
        IEnumerable<TItem> items,
        Func<TItem, string> langSelector
    )
        => items.Select(item => new LangAnnotated<TItem>
        {
            Language = GeneralizeLanguage(langSelector(item)),
            Item = item,
        });

    public string? GeneralizeLanguage(string? lang)
    {
        if (lang == null) return null;

        if (GeneralizeLanguageCallback != null)
        {
            lang = GeneralizeLanguageCallback(lang);
        }

        return lang;
    }
}