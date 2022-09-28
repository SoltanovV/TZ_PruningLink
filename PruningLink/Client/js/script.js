const urlGet = 'https://localhost:7180/api/Home/GetShortUrl';
const urlPostCreate = 'https://localhost:7180/api/Home/CreateShortUrl?longUrl=';
const urlPostDelete = 'https://localhost:7180/api/Home/DeletedUrl/';
const urlRefactorPost = 'https://localhost:7180/RefactorUrl?'

let divContainer = document.querySelector('table.listUrl');

let inputRefactorUrl = document.querySelector('.refactorUrl');
let sendRefactorUrl = document.querySelector('.sendRefactorUrl')
let entUrl = document.querySelector('.sendData')
let iptUrl = document.querySelector('.ent-url')

GetData(urlGet)

function GetData(url){
    fetch(url).then((res) => {
        return res.json()
    }).then((data) =>{
        data.map((u) => {
            let createTable = document.createElement('tr')
            let buttonDeleted = document.createElement('button')
            let buttonDeletedTd = document.createElement('td')
            let buttonRefactor = document.createElement('button')
            let buttonRefactorTd = document.createElement('td')
            let createLongTd = document.createElement('td')
            let createShortTd = document.createElement('td')
            let createCountTd = document.createElement('td')
            let link = document.createElement('a')

            createCountTd.classList.add('count')
            createCountTd.textContent = u.count

            link.classList.add('link')
            link.textContent = u.shortUrl
            link.href = u.shortUrl

            createShortTd.classList.add('urlShort')
            createShortTd.appendChild(link)
            link.addEventListener('click', ()=>{
                location.reload()
            })

            createLongTd.classList.add('urlLong')
            createLongTd.textContent = u.longUrl

            buttonDeleted.classList.add(u.id)
            buttonDeleted.textContent = 'Удалить запись'

            buttonDeletedTd.classList.add('buttonDeletedUrl')
            buttonDeletedTd.appendChild(buttonDeleted)

            buttonRefactor.classList.add(u.id)
            buttonRefactor.textContent = 'Редактировать'
            buttonRefactorTd.classList.add('buttonRefactorUrl')
            buttonRefactorTd.appendChild(buttonRefactor)

            buttonRefactor.addEventListener('click', () =>{
                window.open('RefactorUrl.html')
            })
            if (sendRefactorUrl){
                sendRefactorUrl.addEventListener('click', () =>{
                    let data = inputRefactorUrl.value
                    refactorUrl(buttonRefactor.className, data)
                    alert('Успешно')
                })
            }

            if (divContainer){
                divContainer.appendChild(createTable)
            }
            createTable.append(createCountTd, createShortTd, createLongTd, buttonDeletedTd, buttonRefactorTd)

            buttonDeleted.addEventListener('click',async ()=> {
                await deletedUrl(buttonDeleted.className)
                alert('Успешно')
                location.reload()
            })
        })
    })
}

if (entUrl) {
    entUrl.addEventListener("click",   () => {
        const objRE = /(^https?:\/\/)?/;
        console.log(iptUrl.value)
        if (checkURL(iptUrl.value)){
            const body = document.getElementsByTagName('input')[0].value
            sendUrl(body)
            alert('Успешно!')
            location.reload()
        }
        else{
            alert('Ошибка')
        }
        // Получение данных из input

    });
}
function checkURL(url) {
    const regURL = /^(?:(?:https?|ftp|telnet):\/\/(?:[a-z0-9_-]{1,32}(?::[a-z0-9_-]{1,32})?@)?)?(?:(?:[a-z0-9-]{1,128}\.)+(?:com|net|org|mil|edu|arpa|ru|gov|biz|info|aero|inc|name|[a-z]{2})|(?!0)(?:(?!0[^.]|255)[0-9]{1,3}\.){3}(?!0|255)[0-9]{1,3})(?:\/[a-z0-9.,_@%&?+=\~\/-]*)?(?:#[^ \'\"&<>]*)?$/i;
    return regURL.test(url);
}
// Метод для отправки данных на сервер
function sendUrl(body){
    fetch(urlPostCreate + body, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(res => {
        return res.json()
    })
}

function  deletedUrl(id){
    fetch (urlPostDelete + id, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(res => {
        return res.json()
    })
}

function refactorUrl(id, newUrl){
    fetch (urlRefactorPost + 'id='+ id +'&' + 'url=' + newUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(res => {
        return res.json()
    })
}