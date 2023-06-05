export default() => {
  const formatDate = (date) => {
    if (date === null) {
      return "Ora";
    }
    
    const postDate = new Date(date);
    const currentDate = new Date();
    
    const daysDiff = currentDate.getDate() - postDate.getDate();
    const yearDiff = currentDate.getFullYear() - postDate.getFullYear();
    const monthDiff = currentDate.getMonth() - postDate.getMonth();

    if (((yearDiff === 0 && monthDiff === 0) || (yearDiff === 0 && monthDiff === 1) || (yearDiff === 1 && monthDiff === -11)) && daysDiff > 0) {
      return `${daysDiff} giorn${daysDiff > 1 ? 'i' : 'o'} fa`;
    }
    
    if ((yearDiff === 0 && monthDiff > 0) || (yearDiff === 1 && monthDiff < 0)) {
      return `${monthDiff} mes${monthDiff > 1 ? 'i' : 'e'} fa`;
    }
    
    if (yearDiff > 0) {
      return `${yearDiff} ann${yearDiff > 1 ? 'i' : 'o'} fa`;
    }
    
    return 'Oggi';
  };

  const formatNumber = (number) => {
    const suffixes = ['', 'K', 'M'];

    var suffixIndex = 0;
    var likesCount = number;
    while (likesCount > 999) {
        likesCount = Math.floor(likesCount++ / 1000);
    }

    return likesCount + suffixes[suffixIndex];
  };

  return {
    formatDate,
    formatNumber
  };
}
