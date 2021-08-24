Feature: LiftStatus
Simple service that returns the status of different lifts in a resort

Scenario: Get lifts
	When I ask for a collection of lifts
	Then a collection of lifts are returned with their current status