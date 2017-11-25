<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rss.aspx.vb" Inherits="rss" %><?xml version="1.0" encoding="utf-8"?>
<?xml-stylesheet type="text/xsl" href="viewrss.xsl"?>
<rss version="2.0" xmlns:atom="http://www.w3.org/2005/Atom">
	<channel>
		<title>Что? Где? Когда? - Филадельфия</title>
		<description>Бесплатный сервис новостей и вопросов клуба ЧГК Филадельфия</description>
		<link>http://chtogdekogda.org</link>
		<language>ru</language>
		<copyright>Chto? Gde? Kogda? LLC</copyright>
		<lastBuildDate><%= DateTime.Now %></lastBuildDate>
		<item>
			<pubDate><%= DateTime.Now %></pubDate>
			<link>http://chtogdekogda.org</link>
			<guid><%= FeedId %></guid>
			<description><![CDATA[<%= FeedText %>]]></description>
		</item>
	</channel>
</rss>
