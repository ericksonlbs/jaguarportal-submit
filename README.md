
# Jaguar Portal Submit

Jaguar Portal Submit is a command line application responsible for collecting Spectrum-based Fault Localization (SFL) data generated by the [Jaguar 2](https://github.com/saeg/jaguar2) tool and submitting it to [Jaguar Portal Web](https://github.com/ericksonlbs/JaguarPortal). This application is also available as a GitHub Action, can be used in GitHub Actions and it's available on [GitHub Marketplace](https://github.com/marketplace/actions/jaguarportal-submit).

## Jaguar Portal
Jaguar Portal is a solution composed of a group of tools that together bring the Spectrum-based Fault Localization (SFL) functionality in a continuous integration environment:
- [Jaguar 2](https://github.com/saeg/jaguar2) - JavA coveraGe faUlt locAlization Rank 2 - Jaguar implements the Spectrum-based Fault Localization (SFL) technique for Java programs.
- [Jaguar Portal Web](https://github.com/ericksonlbs/JaguarPortal) - Web API and Web Site responsible for receiving and displaying Spectrum-based Fault Localization (SFL) information.
- [Jaguar Portal Submit](https://github.com/ericksonlbs/jaguarportal-submit) - Command Line responsible for submitting Spectrum-based Fault Localization (SFL) data to the Jaguar Portal Web API. This component is also available as a GitHub Action, for use in GitHub Actions.


## How to configure?

Add jaguar2 dependency on your POM.XML:

    <dependency>
		<groupId>br.usp.each.saeg</groupId>
		<artifactId>jaguar2-junit4</artifactId>
		<version>0.0.2-SNAPSHOT</version>
		<scope>test</scope>
	</dependency>
	<dependency>
		<groupId>br.usp.each.saeg</groupId>
		<artifactId>jaguar2-jacoco-provider</artifactId>
		<version>0.0.2-SNAPSHOT</version>
		<scope>test</scope>
	</dependency>
	<dependency>
		<groupId>br.usp.each.saeg</groupId>
		<artifactId>jaguar2-xml-exporter</artifactId>
		<version>0.0.2-SNAPSHOT</version>
		<scope>test</scope>
	</dependency>

And add plugins:

    <plugin>
		<groupId>org.jacoco</groupId>
		<artifactId>jacoco-maven-plugin</artifactId>
		<version>0.8.7</version>
		<executions>
			<execution>
				<id>prepare-agent</id>
				<goals>
					<goal>prepare-agent</goal>
				</goals>
			</execution>
			<execution>
				<id>report</id>
				<phase>prepare-package</phase>
				<goals>
					<goal>report</goal>
				</goals>
			</execution>
		</executions>
	</plugin>        
	<plugin>
		<groupId>org.apache.maven.plugins</groupId>
		<artifactId>maven-surefire-plugin</artifactId>
		<configuration>				
			<properties>
				<property>
					<name>listener</name>
					<value>br.usp.each.saeg.jaguar2.junit.JaguarJUnitRunListener</value>
				</property>
			</properties>
			<systemPropertyVariables>
				<jaguar2.classesDirs>target/classes</jaguar2.classesDirs>
			</systemPropertyVariables>
		</configuration>
	</plugin>

In your GitHub Actions add Jaguar Portal Submit:

    - name: Submit to Jaguar Portal      
      uses: ericksonlbs/jaguarportal-submit@v0.0.1-snapshot
      if: failure()
      with:
        jaguarPortalProjectKey: {{GENERATED BY JAGUAR PORTAL WEB}}
        jaguarPortalHostUrl: {{URL HOSTED YOUR JAGUAR PORTAL WEB}}
        jaguarPortalClientId: {{GENERATED BY JAGUAR PORTAL WEB}}
        jaguarPortalClientSecret: ${{ secrets.jaguarPortalClientSecret }}
        jaguarPortalAnalysisPath: /target

## How to use?
After running a GitHub Action, if any test fails, Jaguar 2 will generate SFL data in the /target directory, and when Jaguar Portal Submit is run, it will collect this data and send it to the Jaguar Portal Web.

The TOP 10 results can be viewed in Annotations itself, see an example:

![image](https://github.com/user-attachments/assets/b61d3efb-ef73-4363-a566-d46d5c23342c)

You can also view the Pull Request conversation:

![image](https://github.com/user-attachments/assets/eaee7d35-2eb1-4717-a918-119b5357cf16)


Or via the Jaguar Web Portal, where you can view SFL information together with the code, showing suspicious lines marked with colors ranging from green (least suspicious) to red (most suspicious):

![image](https://github.com/user-attachments/assets/9860004d-d8fa-4081-8d12-54c486ce6e28)
