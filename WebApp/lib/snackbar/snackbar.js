/*!
 * snackbar v0.1.10
 * http://polonel.com/snackbar
 *
 * Copyright 2018 Chris Brame and other contributors
 * Released under the MIT license
 * https://github.com/polonel/snackbar/blob/master/LICENSE
 */

(function(root, factory) {
    'use strict';

    if (typeof define === 'function' && define.amd) {
        define([], function() {
            return (root.snackbar = factory());
        });
    } else if (typeof module === 'object' && module.exports) {
        module.exports = root.snackbar = factory();
    } else {
        root.snackbar = factory();
    }
})(this, function() {
    var snackbar = {};

    snackbar.current = null;
    var $defaults = {
        text: 'Default Text',
        textColor: '#FFFFFF',
        width: 'auto',
        showAction: false,
        action:"warning",
        actionText: 'Dismiss',
        actionTextColor: '#4CAF50',
        showSecondButton: false,
        secondButtonText: '',
        secondButtonTextColor: '#4CAF50',
        backgroundColor: '#02B875',
        pos: 'top-right',
        duration: 3000,
        customClass: '',
        onActionClick: function(element) {
            element.style.opacity = 0;
        },
        onSecondButtonClick: function(element) {},
        onClose: function(element) {}
    };

    snackbar.show = function($options) {
        var options = Extend(true, $defaults, $options);

        if (snackbar.current) {
            snackbar.current.style.opacity = 0;
            setTimeout(
                function() {
                    var $parent = this.parentElement;
                    if ($parent)
                    // possible null if too many/fast Snackbars
                        $parent.removeChild(this);
                }.bind(snackbar.current),
                500
            );
        }

        snackbar.snackbar = document.createElement('div');
        snackbar.snackbar.className = 'snackbar-container ' + options.customClass;
        snackbar.snackbar.style.width = options.width;
        var $p = document.createElement('p');
        $p.style.margin = 0;
        $p.style.padding = 0;
        $p.style.color = options.textColor;
        $p.style.fontSize = '14px';
        $p.style.fontWeight = 300;
        $p.style.lineHeight = '1em';
        $p.innerHTML = options.text;
        snackbar.snackbar.appendChild($p);
        if (options.action == "success")
            snackbar.snackbar.style.background = '#02B875';
        else if (options.action == "warning")
            snackbar.snackbar.style.background = '#d9534f';
        else
            snackbar.snackbar.style.background = '#4582EC';

        if (options.showSecondButton) {
            var secondButton = document.createElement('button');
            secondButton.className = 'action';
            secondButton.innerHTML = options.secondButtonText;
            secondButton.style.color = options.secondButtonTextColor;
            secondButton.addEventListener('click', function() {
                options.onSecondButtonClick(snackbar.snackbar);
            });
            snackbar.snackbar.appendChild(secondButton);
        }

        if (options.showAction) {
            var actionButton = document.createElement('button');
            actionButton.className = 'action';
            actionButton.innerHTML = options.actionText;
            actionButton.style.color = options.actionTextColor;
            actionButton.addEventListener('click', function() {
                options.onActionClick(snackbar.snackbar);
            });
            snackbar.snackbar.appendChild(actionButton);
        }

        if (options.duration) {
            setTimeout(
                function() {
                    if (snackbar.current === this) {
                        snackbar.current.style.opacity = 0;
                        // When natural remove event occurs let's move the snackbar to its origins
                        snackbar.current.style.top = '-100px';
                        snackbar.current.style.bottom = '-100px';
                    }
                }.bind(snackbar.snackbar),
                options.duration
            );
        }

        snackbar.snackbar.addEventListener(
            'transitionend',
            function(event, elapsed) {
                if (event.propertyName === 'opacity' && this.style.opacity === '0') {
                    if (typeof(options.onClose) === 'function')
                        options.onClose(this);

                    this.parentElement.removeChild(this);
                    if (snackbar.current === this) {
                        snackbar.current = null;
                    }
                }
            }.bind(snackbar.snackbar)
        );

        snackbar.current = snackbar.snackbar;

        document.body.appendChild(snackbar.snackbar);
        var $bottom = getComputedStyle(snackbar.snackbar).bottom;
        var $top = getComputedStyle(snackbar.snackbar).top;
        snackbar.snackbar.style.opacity = 1;
        snackbar.snackbar.className =
            'snackbar-container ' + options.customClass + ' snackbar-pos ' + options.pos;
    };

    snackbar.close = function() {
        if (snackbar.current) {
            snackbar.current.style.opacity = 0;
        }
    };

    // Pure JS Extend
    // http://gomakethings.com/vanilla-javascript-version-of-jquery-extend/
    var Extend = function() {
        var extended = {};
        var deep = false;
        var i = 0;
        var length = arguments.length;

        if (Object.prototype.toString.call(arguments[0]) === '[object Boolean]') {
            deep = arguments[0];
            i++;
        }

        var merge = function(obj) {
            for (var prop in obj) {
                if (Object.prototype.hasOwnProperty.call(obj, prop)) {
                    if (deep && Object.prototype.toString.call(obj[prop]) === '[object Object]') {
                        extended[prop] = extend(true, extended[prop], obj[prop]);
                    } else {
                        extended[prop] = obj[prop];
                    }
                }
            }
        };

        for (; i < length; i++) {
            var obj = arguments[i];
            merge(obj);
        }

        return extended;
    };

    return snackbar;
});
