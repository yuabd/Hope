
function preview_image(file) {
	_pre_box = $(file).parent();
	_pre_showimg = _pre_box.find('img.pre_showimg');
	/*_pre_showimg = $("#pre_showimg");*/
	// firefox
	if (file['files'] && file['files'][0]) {
		var reader = new FileReader();
		reader.onload = function (e) {
			_pre_showimg.attr({ src: e.target.result });
		}
		reader.readAsDataURL(file.files[0]);
	}
	// ie
	else {
		_pre_showimg.css({ 'display': 'none' });
		_pre_showbox = _pre_box.find('div.pre_showbox');
		file.select();
		_path = document.selection.createRange().text;
		_pre_showbox.css({ "filter": "progid:DXImageTransform.Microsoft.AlphaImageLoader(enabled='true', sizingMethod='scale', src = '" + _path + "')" });
	}

	// remove select
	$("input[name=avatar]").attr("checked", false);
}

function preview_click(e) {
	_pre_box = $(e).parent();
	_pre_file = _pre_box.find('input.pre_file');
	_pre_file.click();

}

function select_img(e, pic) {
	var _e = $(e);
	var _pre_showimg = $("#pre_showimg");
	_pre_showimg.attr('src', _e.attr('src'));

	if (pic) {
		_e.siblings().first().attr("checked", true);
	}

}