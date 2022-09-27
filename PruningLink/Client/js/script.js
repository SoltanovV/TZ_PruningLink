const urlGet = 'https://localhost:7180/api/Home/GetShortUrl'
const urlPost = 'https://localhost:7180/api/Home/CreateShortUrl?longUrl='
const urlPostDeleate = 'https://localhost:7180/DeletedUrl/'

let divContainer = document.querySelector('table.listUrl')

let form = document.querySelector('form')
let entUrl = document.querySelector('input.ent-url')
let btnGetData = document.querySelector('button.getData')
///
btnGetData.addEventListener('click', async() => {
    divContainer.innerHTML = ''
    await GetData(urlGet)

})
///
function GetData(url, longUrl){
    fetch(url).then((res) => {
        return res.json()
    }).then((data) =>{
        // console.log(data)
        data.map((u) => {

            let createTable = createElement('tr', 'dip')
            divContainer.appendChild(createTable)

            let buttonDeleate = document.createElement('button')
            buttonDeleate.classList.add(u.id)

            buttonDeleate.addEventListener('click',  async ()=> {
                console.log(buttonDeleate.className)
                await deletedUrl(buttonDeleate.className)
            })

            let buttonTd = document.createElement('td')
            buttonTd.classList.add('buttonDeleate')
            buttonTd.appendChild(buttonDeleate)

            let createTdShort = document.createElement('td')
            createTdShort.classList.add('urlShort')
            createTdShort.textContent = u.shortUrl

            let createTdLong = document.createElement('td')
            createTdLong.classList.add('urlLong')
            createTdLong.textContent = u.longUrl

            createTable.append(createTdShort, createTdLong, buttonTd)
        })

    })
}

function  deletedUrl(id){
    fetch (urlPostDeleate + id, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    }).then(res => {
        return res
    })
}

form.addEventListener("submit", (e) => {

    // Получение данных из input
    const message = document.getElementsByTagName('input')[0].value
    sendUrl(message)



})
// Метод для отправки данных на сервер
function sendUrl(body){
    fetch(urlPost + body, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(body)
    })
        .then(response => response.json())
}

function createElement(elementTag, elementClass){
    let createTable = document.createElement('elementTag')
    createTable.classList.add('elementClass')
}