window.makeRain = function () {
    //clear out everything
    $('.rain').empty();

    var increment = 0;
    var drops = "";
    var backDrops = "";

    while (increment < 100) {
        //couple random numbers to use for various randomizations
        //random number between 98 and 1
        var randoHundo = (Math.floor(Math.random() * (98 - 1 + 1) + 1));
        //random number between 5 and 2
        var randoFiver = (Math.floor(Math.random() * (5 - 2 + 1) + 2));
        //increment
        increment += randoFiver;
        //add in a new raindrop with various randomizations to certain CSS properties
        drops += '<div class="drop" style="left: ' + increment + '%; bottom: ' + (randoFiver + randoFiver - 1 + 100) + '%; animation-delay: 0.' + randoHundo + 's; animation-duration: 0.5' + randoHundo + 's;"><div class="stem" style="animation-delay: 0.' + randoHundo + 's; animation-duration: 0.5' + randoHundo + 's;"></div><div class="splat" style="animation-delay: 0.' + randoHundo + 's; animation-duration: 0.5' + randoHundo + 's;"></div></div>';
        backDrops += '<div class="drop" style="right: ' + increment + '%; bottom: ' + (randoFiver + randoFiver - 1 + 100) + '%; animation-delay: 0.' + randoHundo + 's; animation-duration: 0.5' + randoHundo + 's;"><div class="stem" style="animation-delay: 0.' + randoHundo + 's; animation-duration: 0.5' + randoHundo + 's;"></div><div class="splat" style="animation-delay: 0.' + randoHundo + 's; animation-duration: 0.5' + randoHundo + 's;"></div></div>';
    }

    $('.rain.front-row').append(drops);
    $('.rain.back-row').append(backDrops);
}

$(document).ready(function () {
    makeRain();
});


var script = document.createElement('script');
script.src = "https://cdn.jsdelivr.net/particles.js/2.0.0/particles.min.js";
script.type = 'text/javascript';
document.getElementsByTagName('head')[0].appendChild(script);

window.MakeFire = function () {
    particlesJS("particles-js", {
        particles: {
            number: {value: 150, density: {enable: true, value_area: 800}},
            color: {
                value: ["#9b4c15", "#E6B033", "#D4932F", "#FFFEFC"]
            },
            shape: {
                type: "circle",
                stroke: {width: 0, color: "#000000"},
                polygon: {nb_sides: 3},
                image: {src: "img/github.svg", width: 100, height: 100}
            },
            opacity: {
                value: 0.5,
                random: false,
                anim: {enable: false, speed: 1, opacity_min: 0.1, sync: false}
            },
            size: {
                value: 2.5,
                random: true,
                anim: {enable: false, speed: 40, size_min: 0.1, sync: false}
            },
            line_linked: {
                enable: false,
                distance: 150,
                color: "#ffffff",
                opacity: 0.4,
                width: 1
            },
            move: {
                enable: true,
                speed: 6,
                direction: "top-right",
                random: false,
                straight: false,
                out_mode: "out",
                bounce: false,
                attract: {enable: false, rotateX: 600, rotateY: 1200}
            }
        },
        interactivity: {
            detect_on: "canvas",
            events: {
                onhover: {enable: false, mode: "bubble"},
                onclick: {enable: false, mode: "push"},
                resize: true
            },
            modes: {
                grab: {distance: 400, line_linked: {opacity: 1}},
                bubble: {distance: 400, size: 40, duration: 2, opacity: 8, speed: 3},
                repulse: {distance: 200, duration: 0.4},
                push: {particles_nb: 4},
                remove: {particles_nb: 2}
            }
        },
        retina_detect: true
    });

    $("#particles-js").css("position", "absolute");
}

/*$(document).on('mousewheel DOMMouseScroll', function (e) {
    if (e.shiftKey) {
        resize();
    }
});

//call the function on drag
$('.panzoom_open').on('panzoompan', function (e, panzoom, x, y) {
    resize();
});
function resize() {
    let element = $('.panzoom_open')
    if (element.length === 0) return;
    let matchX = $('.panzoom_open').css('transform').match(/-?[\d\.]+/g)[4];
    console.log(matchX);
    let x = parseFloat(matchX) / 2;
    element.css('padding-right', 'calc(25vw - ' + x + 'px)');
}*/
