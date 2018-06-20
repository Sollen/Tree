$(document).ready(function() {
    $(".IsRoot .Expand").click(exClick);
    $(".IsRoot .Expand").click();
});  
function addNode(parentNode, item) {

    var expandClass = 'ExpandClosed';
    var type = "Folder";
    var end = '></li>';
    if (item.Type === 0) {
        expandClass = 'ExpandLeaf';
        type = "File";
        end = ' size= ' + item.Size + '></li>';
    }
    var expandDiv = $('<div class="Expand"></div>');
    var content = $('<div class="Content">' + item.Name + '</div>');
    var newNode = $(' <li class="Node ' + expandClass + '" nodeId=' + item.Id + ' nodeType=' + type + ' lastModify=' + item.ModifiDate+ end)
        .append(expandDiv)
        .append(content);
    var container = $('<ul class="Container"> </ul>')
        .append(newNode);
    $(parentNode).append(container);
        
}

function exClick() {
    var node = $(this).parent();
    if (!$(node).length) return;
    $(node).toggleClass("ExpandClosed ExpandOpen");
    if (!$(node).children(".Container").length) {
        $(this).toggleClass("Expand ExpandLoad");
        loadNodes(node);
        $(this).toggleClass("Expand ExpandLoad");
            
    }
}

function loadNodes(parentNode) {
    var parentId = $(parentNode).attr('NodeID');
    $.ajax({
        dataType: "json",
        url: "/GetNodes/" + parentId
    }).done(function (data) {
        var nodes = data.nodes;
        nodes.forEach(function (item) {
            addNode(parentNode, item);
        });
        var child = $(parentNode).find('.Node');
        for (c in child) {
            $(child[c]).children('.Content')
                .unbind('mousemove')
                .unbind('mouseout')
                .mousemove(tooltipShow)
                .mouseout(tooltipHide);

            if (!$(child[c]).hasClass('ExpandLeaf')) {
                $(child[c]).children('.Expand')
                    .unbind('click')
                    .click(exClick);
                    
            };
                
        }
            
    });
}

function tooltipShow(eventObject) {

    var node = $(this).parent();
    $data_tooltip = '<p>Type: '+node.attr("nodeType")+'</p>' +
        '<p>Last Modified: ' +
        new Date(node.attr("lastModify")).toLocaleString("ru") + '</p>';

    if (node.attr("size") != null) {
        $data_tooltip += '<p>Size: ' + node.attr("size") / 1000 + 'Кб</p>';
    }
    $('#tooltip').empty();
    $('#tooltip').append($data_tooltip)
        .css({
            'top': eventObject.pageY + 20,
            'left': eventObject.pageX + 5
        })
        .show();
}

function tooltipHide() {

    $("#tooltip").hide()
        .empty()
        .css({
            "top": 0,
            "left": 0
        });
}
