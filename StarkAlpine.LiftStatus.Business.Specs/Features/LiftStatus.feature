Feature: LiftStatus
Simple service that returns the status of different lifts in a resort

Scenario: Lifts are closed out of hours
	Given the following resort:
	| Field       | Value             |
	| TimeZone    | America/Vancouver |
	| OpeningTime | 0800              |
	| ClosingTime | 1600              |
	And the current UTC time is 1500
	When I ask for a collection of lifts
	Then lifts should all be closed

Scenario: Lifts are running when the resort is open
	Given the following resort:
	| Field       | Value             |
	| TimeZone    | America/Vancouver |
	| OpeningTime | 0800              |
	| ClosingTime | 1600              |
	And the current UTC time is 2000
	When I ask for a collection of lifts
	Then lifts should not be closed