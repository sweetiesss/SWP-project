// JavaScript source code
var postApi = 'https://localhost:7219/api/assignments';
fetch(postApi).then(function (pots) {
    return pots.json();
}).then(function (result) {
    var htmls = result.map(function (obj) {
        return `<li><h2>${obj.id}</h2><p>${obj.name}</p></li>`;
    });
    var html = htmls.join('');
    document.getElementById('root').innerHTML = html; 
});