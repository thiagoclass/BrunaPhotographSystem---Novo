const animateCSS = (element, animation, prefix = 'animate__') =>
    // We create a Promise and return it
    new Promise((resolve, reject) => {
        const animationName = `${prefix}${animation}`;
        const node = document.querySelector(element);

        node.classList.remove(`${prefix}animated`, `${prefix}tada`, `${prefix}delay-5s`, `${prefix}delay-4s`, `${prefix}delay-3s`, `${prefix}delay-2s`);

        node.classList.add(`${prefix}animated`, animationName);

        // When the animation ends, we clean the classes and resolve the Promise
        function handleAnimationEnd() {
            node.classList.remove(`${prefix}animated`, animationName);
            node.removeEventListener('animationend', handleAnimationEnd);

            resolve('Animation ended');
        }

        node.addEventListener('animationend', handleAnimationEnd);
    });

$(".EfeitoHoverTrabalho").hover(
    function () {
        animateCSS('.EfeitoHoverTrabalho', 'bounce');
    }, function () {
        animateCSS('.EfeitoHoverTrabalho', 'tada');
    }
);

$(".EfeitoHoverInicio").hover(
    function () {
        animateCSS('.EfeitoHoverInicio', 'bounce');
    }, function () {
        animateCSS('.EfeitoHoverInicio', 'tada');
    }
);

$(".EfeitoHoverLogin").hover(
    function () {
        animateCSS('.EfeitoHoverLogin', 'bounce');
        $(this).removeClass('Sombra');
    }, function () {
        animateCSS('.EfeitoHoverLogin', 'tada');
        $(this).addClass('Sombra');
    }
);

$(".EfeitoHoverSobre").hover(
    function () {
        animateCSS('.EfeitoHoverSobre', 'bounce');   
    }, function () {
        animateCSS('.EfeitoHoverSobre', 'tada');
    }
);


//// class dos links que vão receber o click
//$(".link-menu").on("click", function (event) {

//    // a animação vai ocorrer no html, body
//    $('html, body').animate({

//        // pega o atributo href do this (link que recebeu o click)
//        // e faz a animação com velocidade 1000 para o destino do href;
//        scrollTop: $($(this).attr("href")).offset().top
//    }, 1000);

//    // Faz com que o destino do link não vá para a url
//    event.preventDefault();
//});