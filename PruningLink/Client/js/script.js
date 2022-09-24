const url = 'https://localhost:7180/api/Home/GetShortUrl'
fetch(url).then((res) => {
    return res.json()
}).then((data) =>{
    console.log(data)
    data.map((u) => {
        let divContainer = document.getElementById('table')
        let createCoursUl = document.createElement('h1')
        createCoursUl.classList.add('three')
        createCoursUl.setAttribute('id', 'three')
        divContainer.appendChild(createCoursUl)  
        createCoursUl.textContent = u.shortUrl
    })

})

let form = document.querySelector('form')
let entUrl = document.querySelector('input.ent-url')

form.addEventListener("submit", (e) => {

    e.preventDefault();
    var res = /(http(s)?:\/\/.)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/
    if(!res.test(entUrl.value)){
        entUrl.style.background = 'red'
        entUrl.focus()
        return
    }
    alert(entUrl)
})