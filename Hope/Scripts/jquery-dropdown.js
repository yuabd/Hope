/*
* Autuor : muxiu 373259115@qq.com
*/

(function ($) {

	$.fn.dropdown = function (a_options) {

		// get object
		var _dropdown_box = this.find(".dropdown_box");     // find .dropdown_box
		var _dropdown_left = this.find(".dropdown_left");   // find .dropdown_left
		var _ul = this.find("ul");
		var _lis = this.find("li");
		var _input = this.find("input");

		var options = $.extend($.fn.dropdown.defaults, a_options);

		// _dropdown_box click
		_dropdown_box.click(function () {
			_ul.toggle();
		});

		// li click
		_lis.click(function () {
			_ul.toggle();
			// change value
			changevalue(this);
		});

		function changevalue(e) {
			_e = $(e);
			_dropdown_left.text(_e.text());
			_input.val(_e.attr('value'));
		}

		// init value
		if (options['index'] == '')
			changevalue(_lis[0]);
		else
			changevalue(this.find("li[value=" + options['index'] + "]"));

	};

	$.fn.dropdown.defaults = {
		index: ''
	};

})(jQuery)