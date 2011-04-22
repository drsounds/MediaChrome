<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/">
	<view>
		<section name="Overview">
			
			<element type="sp:section">
					<attribute name="text" value="Test"/>
			</element>
			
			<xsl:for-each select="artist/album">
				<element name="divider">
					<attribute name="distance" value="80"/>
				</element>
				<element type="sp:image">
					<attribute name="src">						
						<xsl:attribute name="value">
							<xsl:value-of select="@coverid"/>
						</xsl:attribute>
					</attribute>
					<attribute name="left" value="10"/>
					<attribute name="position" value="absolute"/>
					<attribute name="top" value="@top"/>
					<attribute name="width" value="120"/>
					<attribute name="height" value="120"/>
					<attribute />
				</element>
				<element type="sp:header">
					<attribute name="title">
						<xsl:attribute name="value">	
							<xsl:value-of select="album/@title"/>
						</xsl:attribute>
					</attribute>
					<attribute name="link" value="true"/>
				</element>
				<!--
				<element type="sp:entry">
					<attribute name="title" value="a"/>
					<attribtue name="author" value="b"/>
				</element>
				<element type="sp:space">
					<attribute name="distance" value="80"/>
				</element>-->
			</xsl:for-each>
		</section>
	</view>
</xsl:template>

</xsl:stylesheet>