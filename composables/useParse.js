export default() => {
  const parsePost = (postContent) => {
    const components = postContent.split('---\n');
    if (components.length != 3) {
      return {
        content: postContent,
        data: {
          image: ''
        }
      }
    }

    // Message should be in the format ---\nimaage: 'URL'\n---\nHTML_CONTENT
    const parsedObj = {
      content: components[2],
      data: { }
    };
    
    for (const row of components[1].split('\n')) {
      if (row != '') {
        var key = '';
        var stringIndex = 0;
            
        while (stringIndex < row.length && row[stringIndex] != ':') {
          key += row[stringIndex++];
        }
            
        while (stringIndex < row.length && row[stringIndex] != '\'' && row[stringIndex] != '"'){
          stringIndex++;
        }

        var closingTagIndex = row.length - 1;
        while (stringIndex < row.length && row[closingTagIndex] != '\'' && row[closingTagIndex] != '"') {
          closingTagIndex--;
        }

        parsedObj.data[key] = row.substring(++stringIndex, closingTagIndex);
      }
    }
    return parsedObj;
  };

  return {
    parsePost
  };
}
