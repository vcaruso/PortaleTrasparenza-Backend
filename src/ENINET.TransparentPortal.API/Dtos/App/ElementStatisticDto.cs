namespace ENINET.TransparentPortal.API.Dtos.App;

public record class ElementStatisticDto(string element, IList<int> monthValues);
