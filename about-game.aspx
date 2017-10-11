<%@ Page Language="VB" AutoEventWireup="false" CodeFile="about-game.aspx.vb" Inherits="about_game" MasterPageFile="~/MasterPage.master" %>


<asp:Content runat="server" ContentPlaceHolderID="ContentArea">

	<section>
		<h1>Что нужно знать</h1>

		<p>
			Сбор в 5:30 PM; в 5:45 PM формируем команды и рассаживаем новых игроков. В 6 вечера начнётся сама игра. Не опаздывайте!
			Вы можете прийти в любом количестве, от одного человека. Заранее регистрироваться нигде не нужно. 
			Если вы никогда еще не играли и у вас нет команды, то не беспокойтесь, мы сформируем вам команду на месте из таких же как вы. 
			Требования к командам - от одного до 6 человек. Да, можно играть даже в одиночестве, но на практике так практически никогда не происходит -
			новички объединяются в новые команды. 
		</p>
		<p>
			Игра длится около 4 часов, в двумя перерывами. Будет 3 тура по 10 вопросов в каждом. В стоимость мероприятия входит чай, кофе, вода (на столах), легкие закуски. 
			В перерыве будет подана пицца. Регистрироваться на игру или покупать билеты не нужно, оплата производится на месте. Стоимость $15 (cash only) с человека.
		</p>
		<p>
			Хочется отметить, что не нужно обладать никакими особыми знаниями. "Что? Где? Когда?" - это командрая игра, а истинные вопросы ЧГК не на знание, а на логику.
			Кроме того, играть в ЧГК не просто полезно, но и весело, интересно, позновательно. Многие нашли себе новых друзей благодаря нашей игре. Приходите и Вы. Не пожалеете!
		</p>


	</section>

	<section id="map">
		<h1>Как проехать на игру</h1>

		<img src="Img/kleinlife.png" style="float: right" />
		Игра проходит в <a target="_blank" href="http://kleinlife.org/">KleinLife</a> (бывшее JCC).
		<br />
		Адрес: <strong>10100 Jamison Ave, Philadelphia, PA 19116</strong><br />
		Вы можете посмотреть расположение на карте:
		<div>
			<script src='https://maps.googleapis.com/maps/api/js?v=3.exp&key=AIzaSyC22jZM9wTvUrXS53XTSiUL_lBzZCeOqpQ'></script>
			<div style='overflow: hidden; height: 400px; width: 520px; margin: auto'>
				<div id='gmap_canvas' style='height: 400px; width: 520px;'></div>
				<style>
					#gmap_canvas img {
						max-width: none !important;
						background: none !important;
					}
				</style>
			</div>
			<script type='text/javascript' src='https://embedmaps.com/google-maps-authorization/script.js?id=8a8ce501bc2ac6ed7db371b3b38b2028417724a7'></script>
			<script type='text/javascript'>function init_map() { var myOptions = { zoom: 12, center: new google.maps.LatLng(40.09918112610077, -75.02015869682009), mapTypeId: google.maps.MapTypeId.ROADMAP }; map = new google.maps.Map(document.getElementById('gmap_canvas'), myOptions); marker = new google.maps.Marker({ map: map, position: new google.maps.LatLng(40.09918112610077, -75.02015869682009) }); infowindow = new google.maps.InfoWindow({ content: '<strong>Что? Где? Когда?</strong><br>10100 Jamison Ave<br>19116 Philadelphia<br>' }); google.maps.event.addListener(marker, 'click', function () { infowindow.open(map, marker); }); infowindow.open(map, marker); } google.maps.event.addDomListener(window, 'load', init_map);</script>
		</div>
	</section>



</asp:Content>
